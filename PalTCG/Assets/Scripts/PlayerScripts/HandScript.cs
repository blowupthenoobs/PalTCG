using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

using DefaultUnitData;

public class HandScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public GamePreferences Preferences; //Temporarilly set to public

    public static HandScript Instance;

    //Hand Stuff
    public GameObject selected;
    private GameObject target;

    public new List<GameObject> Hand = new List<GameObject>();

    //Card Stuff
    public string state;
    public List<GameObject> selection = new List<GameObject>();
    [SerializeField] GameObject cardPrefab;
    public UnityAction updateSelection;

    //Moving stuff
    private Vector3 originalPos;
    private Vector3 duckPos;
    private Vector3 targetPos;
    public float duckAmount;
    public float moveSpeed;

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
        
        originalPos = transform.position;
        duckPos = new Vector3(originalPos.x, originalPos.y - duckAmount * ScreenCalculations.GetScale(gameObject), originalPos.z);
        targetPos = duckPos;
        transform.position = targetPos;
    }

    void Update()
    {
        RemoveIndexes();
        CenterCards();

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
        switch(state)
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

                break;
            case "choosingAttack":
                selected = card;
                state = "targeting";
                break;
            case "raiding":
                selection.Add(card);
                updateSelection.Invoke();
                break;
            default:
                Debug.Log("invalid state");
                break;
        }
        
    }

    public void ClearSelection()
    {
        selected = null;
        while(selection.Count > 0)
        {
            selection[0].SendMessage("Deselect");
            selection.RemoveAt(0);
        }
    }

    public void Attack()
    {
        for(int i = 0; i < selection.Count; i++)
        {
            selection[i].SendMessage("Attack", selected);
            Debug.Log("put " + selection[i] + " to sleep");
        }

        ClearSelection();
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
    public void CenterCards()
    {
        float spacing = Preferences.spacing * ScreenCalculations.GetScale(gameObject);

        float speed = Preferences.cardMoveSpeed * ScreenCalculations.GetScale(gameObject);

        for(int i = 0; i < Hand.Count; i++)
        {
            RectTransform rectTransform = Hand[i].GetComponent<RectTransform>();
            Vector3 newPosition = rectTransform.localPosition;
            newPosition.x = spacing * ((i+1) - (float)(Hand.Count + 1) / 2 );
            newPosition.x = Mathf.Lerp(Hand[i].GetComponent<RectTransform>().localPosition.x, newPosition.x, speed * Time.deltaTime);
            rectTransform.localPosition = newPosition;
        }
    }

    public void Draw(string datatype)
    {
        string[] dataParts = datatype.Split("/");

        var newCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        CardData data = null;

        switch(dataParts[0])
        {
            case "p":
                data = (PalCardData)ScriptableObject.CreateInstance(typeof(PalCardData));
                ((PalCardData)data).DecomposeData(new Pals().FindPalData(dataParts[1], int.Parse(dataParts[2])));
            break;
            case "t":
                Debug.Log("look for items");
            break;
            case "h":
                Debug.Log("look for player/hero");
            break;
        }

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
