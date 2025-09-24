using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;

using Resources;
using DefaultUnitData;
public class HandScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public GamePreferences Preferences; //Temporarilly set to public

    public static HandScript Instance;
    public PhotonView opponentMirror;

    //Hand Stuff
    public GameObject selected;
    private GameObject target;

    public List<GameObject> Hand = new List<GameObject>();
    public List<GameObject> BuildingDeck = new List<GameObject>();
    private bool holdingBuildings;

    //Card Stuff
    public string state;
    public List<GameObject> selection = new List<GameObject>();
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject buildingPrefab;
    public UnityAction updateSelection;

    //Moving stuff
    private Vector3 originalPos;
    private Vector3 duckPos;
    private Vector3 targetPos;
    public float duckAmount;
    public float moveSpeed;

    //Attack Checks
    public GameObject currentAttacker;
    public List<GameObject> raid = new List<GameObject>();
    public List<GameObject> attackers = new List<GameObject>();
    public GameObject blocker;
    public bool targetWasNull;

    //Board References
    public GameObject playerCard;
    public List<GameObject> cardSlots = new List<GameObject>();
    public GameObject playerDrawPile;
    public GameObject playerDiscardPile;

    //Online Stuffs
    private bool readyForNextAttackAction;

    //Misc
    public resources GatheredItems;
    public CardData tempDataTypeRef;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        originalPos = transform.position;
        duckPos = new Vector3(originalPos.x, originalPos.y - duckAmount * ScreenCalculations.GetScale(gameObject), originalPos.z);
        targetPos = duckPos;
        transform.position = targetPos;
        opponentMirror = this.GetComponent<PhotonView>();
    }

    void Start()
    {
        CreateBuildingCards();
    }

    void Update()
    {
        RemoveIndexes();
        CenterNormalCards();
        CenterBuildingCards();

        if(Input.GetButtonDown("Fire1"))
        {
            if(selected != null && state == "default")
                StartCoroutine(Click());
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * ScreenCalculations.GetScale(gameObject) * Time.deltaTime);

    }

#region Selecting&Targeting
    public void Select(GameObject card)
    {
        switch (state)
        {
            case "default":
                selected = card;
                Duck();

                break;
            case "buildingPay":
                if(card != selected)
                {
                    if(!selection.Contains(card))
                        selection.Add(card);
                    else
                    {
                        selection.RemoveAt(selection.IndexOf(card));
                        card.SendMessage("Deselect");
                    }

                    updateSelection.Invoke();
                }

                break;
            case "lookingForSphere":
                if(card.GetComponent<PalSphereScript>() != null)
                {
                    selection.Clear();
                    selection.Add(card);

                    updateSelection.Invoke();
                }
                else if(card.GetComponent<ToolCardScript>() != null)
                {
                    if(((ToolCardData)card.GetComponent<ToolCardScript>().cardData).toolType == "ride")
                    {
                        selection.Clear();
                        selection.Add(card);

                        updateSelection.Invoke();
                    }
                }

                break;
            case "lookingForSlot":
                if(card.GetComponent<ToolSlotScript>() != null)
                {
                    selection.Clear();
                    selection.Add(card);

                    updateSelection.Invoke();
                }

                break;
            case "choosingAttack":
                selected = card;
                state = "targeting";
                break;
            case "raiding":
                selection.Add(card);
                updateSelection.Invoke();
                break;
            case "settingAilment":
                if(!selection.Contains(card))
                {
                    if(updateSelection == AllowConfirmations.LookForSingleTarget && selection.Count > 0)
                    {
                        selection[0].SendMessage("Deselect");

                        selection.Clear();
                    }

                    selection.Add(card);
                    card.SendMessage("Select");
                }
                else
                {
                    selection.RemoveAt(selection.IndexOf(card));
                    card.SendMessage("Deselect");
                }

                updateSelection.Invoke();
                break;
            case "blocking":
                if(card.GetComponent<UnitCardScript>().cardData.traits.tags.Contains("blocker"))
                {
                    selection.Clear();
                    selection.Add(card);

                    updateSelection.Invoke();
                }
                break;
            case "endOfTurnAbilities":
                if(selected != null)
                    selected.SendMessage("Deselect");
                
                selected = card;
                updateSelection.Invoke();
                break;
            case "selectingEffectForFriendly":
                if(!selection.Contains(card))
                {
                    selection.Add(card);
                }
                else
                {
                    selection.RemoveAt(selection.IndexOf(card));
                    card.SendMessage("Deselect");
                }
                updateSelection.Invoke();
                break;
            case "choosingCardInDiscard":
                if(card.GetComponent<PileViewCardIconScript>().cardData == tempDataTypeRef)
                {
                    card.SendMessage("Deselect");
                    tempDataTypeRef = null;
                }
                else
                {
                    if(tempDataTypeRef != null)
                        playerDiscardPile.SendMessage("DeselectItemOfDataType", tempDataTypeRef);
                    
                    tempDataTypeRef = card.GetComponent<PileViewCardIconScript>().cardData;
                    card.SendMessage("Select");
                }
                updateSelection.Invoke();
                break;
            default:
                Debug.Log("invalid state: " + state);
                break;
        }

    }

    public void ClearSelection()
    {
        if(selected != null)
            selected.SendMessage("Deselect");
        selected = null;

        if(selection.Count > 0)
            UnselectSelection();
    }

    public void UnselectSelection()
    {
        while(selection.Count > 0)
        {
            selection[0].SendMessage("Deselect");
            selection.RemoveAt(0);
        }
    }

    public IEnumerator Attack()
    {
        raid = new List<GameObject>(selection);
        attackers = new List<GameObject>(raid);
        var blockList = new List<GameObject>();

        UnselectSelection();

        for(int i = 0; i < attackers.Count; i++)
        {
            currentAttacker = attackers[i];
            FieldAbilityHandlerScript.Instance.enemyHandler.RPC("LookForBlockers", RpcTarget.Others);
            yield return new WaitUntil(() => readyForNextAttackAction);
            readyForNextAttackAction = false;
            attackers[i].SendMessage("Attack");
            yield return new WaitUntil(() => currentAttacker == null);
            if(!targetWasNull)
                attackers[i].SendMessage("Rest");
            targetWasNull = false;
            if(blocker != null)
            {
                blocker.SendMessage("SendRestEffect");
                blockList.Add(blocker);
            }
            blocker = null;
        }


        while(attackers.Count > 0)
        {
            attackers[0].SendMessage("Deselect");
            attackers.RemoveAt(0);
        }
        raid.Clear();

        while(blockList.Count > 0)
        {
            blockList[0].SendMessage("AfterBlockActions");
            blockList.RemoveAt(0);
        }

        //Mirror thing for attackers with blockers

        selected = null;
        state = "choosingAttack";
    }

    [PunRPC]
    public void PassAction()
    {
        readyForNextAttackAction = true;
    }

    public IEnumerator Click()
    {
        var originalSelect = selected;

        yield return new WaitForSeconds(.15f);

        if(selected != null && state == "default")
        {
            if(originalSelect == selected)
            {
                selected.SendMessage("Deselect");
                selected = null;
            }
            else
            {
                originalSelect.SendMessage("Deselect");
            }

        }
    }
#endregion

#region HandStuff
    public void CreateBuildingCards()
    {
        foreach(string building in AccountManager.Instance.player.earnedItems.ownedBuildingTypes)
        {
            var newCard = Instantiate(buildingPrefab, transform.position, transform.rotation);

            newCard.GetComponent<SchematicScript>().cardData = (BuildingData)ScriptableObject.CreateInstance(typeof(BuildingData));
            newCard.GetComponent<SchematicScript>().cardData.SetUpData(building);
            newCard.GetComponent<SchematicScript>().SetUpCard();
            BuildingDeck.Add(newCard);
            newCard.transform.SetParent(gameObject.transform);

            RectTransform rectTransform = newCard.GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.y = 0;
            rectTransform.localPosition = newPosition;
            newCard.SetActive(false);
        }
    }

    public void CenterNormalCards()
    {
        float spacing = DetermineCardSpacing();

        float speed = Preferences.cardMoveSpeed * ScreenCalculations.GetScale(gameObject);

        for(int i = 0; i < Hand.Count; i++)
        {
            RectTransform rectTransform = Hand[i].GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.x = spacing * ((i + 1) - (float)(Hand.Count + 1) / 2);
            newPosition.x = Mathf.Lerp(Hand[i].GetComponent<RectTransform>().localPosition.x, newPosition.x, speed * Time.deltaTime);
            rectTransform.localPosition = newPosition;
        }
    }

    public void CenterBuildingCards()
    {
        float spacing = DetermineCardSpacing();

        float speed = Preferences.cardMoveSpeed * ScreenCalculations.GetScale(gameObject);

        for(int i = 0; i < BuildingDeck.Count; i++)
        {
            RectTransform rectTransform = BuildingDeck[i].GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.x = spacing * ((i + 1) - (float)(BuildingDeck.Count + 1) / 2);
            newPosition.x = Mathf.Lerp(BuildingDeck[i].GetComponent<RectTransform>().localPosition.x, newPosition.x, speed * Time.deltaTime);
            rectTransform.localPosition = newPosition;
        }
    }

    public void SwitchHands()
    {
        if(holdingBuildings)
            SwitchToHand();
        else
            SwitchToBuildingDeck();
    }

    public void SwitchToBuildingDeck()
    {
        foreach(GameObject card in Hand)
        {
            card.SetActive(false);
        }

        foreach(GameObject card in BuildingDeck)
        {
            card.SetActive(true);
        }

        holdingBuildings = true;
    }

    public void SwitchToHand()
    {
        foreach(GameObject card in BuildingDeck)
        {
            card.SetActive(false);
        }
        
        foreach(GameObject card in Hand)
        {
            card.SetActive(true);
        }

        holdingBuildings = false;
    }

    private float DetermineCardSpacing()
    {
        return (Preferences.spacing * ScreenCalculations.GetScale(gameObject));
    }

    public void Draw(CardData data)
    {
        var newCard = Instantiate(cardPrefab, transform.position, transform.rotation);

        newCard.GetComponent<CardScript>().cardData = data;
        newCard.GetComponent<CardScript>().SetUpCard();
        Hand.Add(newCard);
        newCard.transform.SetParent(gameObject.transform);

        RectTransform rectTransform = newCard.GetComponent<RectTransform>();
        Vector3 newPosition = rectTransform.localPosition;
        newPosition.y = 0;
        rectTransform.localPosition = newPosition;
    }

    public void Discard(GameObject card)
    {
        playerDiscardPile.SendMessage("DiscardCard", card.GetComponent<CardScript>().cardData);
        Hand.RemoveAt(Hand.IndexOf(card));
        Destroy(card);
    }

    private void RemoveIndexes()
    {
        for(int i = 0; i < Hand.Count; i++)
        {
            if(Hand[i] == null)
                Hand.RemoveAt(i);
        }
    }
#endregion

#region HandMovement
    public void OnPointerEnter(PointerEventData eventData)
    {
        Raise();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Duck();
    }

    public void Duck()
    {
        targetPos = duckPos;
    }

    public void Raise()
    {
        targetPos = originalPos;
    }
#endregion
}
