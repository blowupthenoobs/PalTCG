using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Photon.Pun;
using TMPro;

using Resources;
public class UnitCardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Image image;
    protected Button button;
    public CardData cardData;
    public Color normalColor; //Probably Temp
    public Color selectColor;
    [SerializeField] protected TMP_Text health;
    [HideInInspector] public PhotonView opponentMirror;
    protected GameObject heldCard;


    protected UnityAction StartPlayerTurn;
    protected UnityAction StartEnemyTurn;
    protected UnityAction StartPlayerAttack;
    protected UnityAction StartEnemyAttack;
    protected UnityAction EndPlayerTurn;
    protected UnityAction EndEnemyTurn;

    //Effects and state variables
    public bool resting;
    public bool palSKillActive;

    //Coroutine Checks
    protected bool readyForNextAttackAction;
    protected bool moveRejected;
    protected bool movePassedThrough;

    public StatusEffects statuses;
    public int turnsOnSpot;

    private bool hovered;
    private bool viewButtonPressed;


    void Awake()
    {
        button = GetComponent<Button>();
        image = gameObject.GetComponent<Image>();

        GiveCardEventActions();
    }

    void Update()
    {
        CheckForAltPress();

        if(viewButtonPressed && hovered)
            LargeCardViewScript.Instance.FocusCard(cardData.cardArt, gameObject);

    }

    public void SetUpCard(CardData newData)
    {
        cardData = newData;
        image.sprite = cardData.cardArt;
        cardData.currentHp = cardData.maxHp;
        health.text = cardData.currentHp.ToString();
    }

    public void PlaceOnSpot()
    {
        opponentMirror.RPC("UpdateHealth", RpcTarget.Others, cardData.currentHp);
        GiveTraitsToBuildings();
        SetUpBasicTurnEvents();
    }

    public void Hurt(int dmg)
    {
        if(heldCard == null)
        {
            cardData.currentHp -= dmg;

            if(cardData.currentHp <= 0)
                Die();

            health.text = cardData.currentHp.ToString();
            opponentMirror.RPC("UpdateHealth", RpcTarget.Others, cardData.currentHp);
        }
        else
            heldCard.SendMessage("Hurt", dmg);
    }

    public void Heal(int heal)
    {
        if(heldCard == null)
        {
            cardData.currentHp += heal;

            if(cardData.currentHp > cardData.maxHp)
                cardData.currentHp = cardData.maxHp;
        }
        else
            heldCard.SendMessage("Heal", heal);

    }

    public void PrepareAttackPhase()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(SelectForAttack);
    }

    public void PrepareEnemyPhases()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(EnemyTurnActions);
    }

    public IEnumerator Attack()
    {
        GameObject target;

        if(HandScript.Instance.blocker == null)
            target = HandScript.Instance.selected;
        else
            target = HandScript.Instance.blocker;

        if(cardData.WhenAttack != null)
        {
            for(int i = 0; i < cardData.WhenAttack.Count; i++)
            {
                cardData.WhenAttack[i].Invoke();
                yield return new WaitUntil(() => readyForNextAttackAction);
                readyForNextAttackAction = false;
            }
        }

        if(cardData.WhenSkillAttack != null && palSKillActive)
        {
            for(int i = 0; i < cardData.WhenSkillAttack.Count; i++)
            {
                cardData.WhenSkillAttack[i].Invoke();
                yield return new WaitUntil(() => readyForNextAttackAction);
                readyForNextAttackAction = false;
            }
        }

        if(target != null)
            target.SendMessage("Hurt", cardData.currentAtk);
        else
            HandScript.Instance.targetWasNull = true;

        if(cardData.OnAttack != null)
        {
            for(int i = 0; i < cardData.OnAttack.Count; i++)
            {
                cardData.OnAttack[i].Invoke();
                yield return new WaitUntil(() => readyForNextAttackAction);
                readyForNextAttackAction = false;
            }
        }

        if(cardData.OnSkillAttack != null && palSKillActive)
        {
            for(int i = 0; i < cardData.OnSkillAttack.Count; i++)
            {
                cardData.OnSkillAttack[i].Invoke();
                yield return new WaitUntil(() => readyForNextAttackAction);
                readyForNextAttackAction = false;
            }
        }

        yield return null;
        HandScript.Instance.currentAttacker = null;
    }

    public IEnumerator UseActivatedAbility()
    {
        if(cardData.OncePerTurn != null)
        {
            for(int i = 0; i < cardData.OncePerTurn.Count; i++)
            {
                if(cardData.AbilityUseCounter[i] == 0)
                {
                    cardData.OncePerTurn[i].Invoke();
                    yield return new WaitUntil(() => (movePassedThrough || moveRejected));

                    if(movePassedThrough)
                        cardData.AbilityUseCounter[i]++;

                    movePassedThrough = false;
                    moveRejected = false;
                }

            }
        }
    }

    public void DontUseAbility()
    {
        moveRejected = true;
    }

    public void UseAbility()
    {
        movePassedThrough = true;
    }

    public bool CanUseAbilities()
    {
        if(cardData.OncePerTurn == null)
            return false;

        if(!cardData.AbilityUseCounter.Contains(0))
            return false;

        return true;
    }

    public void FinishEffect()
    {
        readyForNextAttackAction = true;
    }

    public void ResetPhase()
    {
        button.onClick.RemoveAllListeners();
    }

    public void SelectForAttack()
    {
        if(!resting)
        {
            if(HandScript.Instance.state == "choosingAttack" || HandScript.Instance.state == "targeting")
            {
                if(HandScript.Instance.selected != gameObject && !HandScript.Instance.selection.Contains(gameObject))
                {
                    image.color = selectColor;
                    HandScript.Instance.Select(gameObject);
                }
                else
                    RemoveFromSelection();
            }
            else if(HandScript.Instance.state == "raiding")
            {
                if(HandScript.Instance.selected != gameObject && !HandScript.Instance.selection.Contains(gameObject))
                {
                    image.color = selectColor;
                    HandScript.Instance.Select(gameObject);
                }
                else
                    RemoveFromSelection();
            }
            else
                RemoveFromSelection();
        }
    }

    public void EnemyTurnActions()
    {
        if(HandScript.Instance.state == "blocking" && cardData.traits.blocker)
            HandScript.Instance.Select(gameObject);
        // else if()
    }

    public void RemoveFromSelection()
    {
        if(HandScript.Instance.selected == gameObject)
        {
            HandScript.Instance.selected = null;

            if(HandScript.Instance.state == "targeting")
                HandScript.Instance.state = "choosingAttack";
        }
        if(HandScript.Instance.selection.Contains(gameObject))
            HandScript.Instance.selection.Remove(gameObject);

        image.color = normalColor;
    }

    public void Rest()
    {
        if(heldCard == null)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            resting = true;
            opponentMirror.RPC("NormalRest", RpcTarget.Others);
        }
        else
            heldCard.SendMessage("Rest");

    }

    public void Wake()
    {
        if(heldCard == null)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            resting = false;
            opponentMirror.RPC("Wake", RpcTarget.Others);
        }
        else
            heldCard.SendMessage("Wake");
    }

    public void AfterBlockActions()
    {
        if(cardData.traits.tank)
            Wake();
    }

    protected virtual void Die()
    {
        SendToPalBox();
    }

    protected virtual void RemoveFromSphere()
    {
        transform.parent.gameObject.SendMessage("LoseHeldCard");
        RemoveCardEventsFromManager();
        RemoveTraitsFromBuilding();
        Destroy(gameObject);
    }

    public virtual void SendToPalBox()
    {
        HandScript.Instance.playerDiscardPile.SendMessage("DiscardCard", cardData);
        opponentMirror.RPC("HeldUnitDeath", RpcTarget.Others);
        RemoveFromSphere();
    }

    public void Deselect()
    {
        image.color = normalColor;
    }

    public void OpenContextMenu(GameObject caller)
    {
        transform.parent.SendMessage("OpenContextMenu", caller);
    }

    public virtual bool CanBeBooted()
    {
        return (turnsOnSpot > 0);
    }

    public virtual void GiveCardEventActions()
    {
        StartPlayerTurn += Wake;
        StartPlayerAttack += PrepareAttackPhase;
        StartEnemyTurn += Wake;
        StartEnemyTurn += PrepareEnemyPhases;
    }

    public void SetUpBasicTurnEvents()
    {
        GameManager.Instance.StartPlayerTurn += StartPlayerTurn;
        GameManager.Instance.StartPlayerAttack += StartPlayerAttack;
        GameManager.Instance.StartEnemyTurn += StartEnemyTurn;

        StartPlayerTurn += ResetPalSkill;
        StartPlayerTurn += () => turnsOnSpot++;
    }

    public void HideHealthCounter()
    {
        health.text = "";
    }

    public void ResetPalSkill()
    {
        palSKillActive = false;
    }

    public void RemoveCardEventsFromManager()
    {
        GameManager.Instance.StartPlayerTurn -= StartPlayerTurn;
        GameManager.Instance.StartPlayerAttack -= PrepareAttackPhase;
        GameManager.Instance.StartEnemyTurn -= StartPlayerAttack;
        GameManager.Instance.StartEnemyTurn -= StartEnemyTurn;
    }

    public void GiveTraitsToBuildings()
    {
        BuildingScript.totalTraits.handyWork += cardData.traits.handyWork;
        BuildingScript.totalTraits.foraging += cardData.traits.foraging;
        BuildingScript.totalTraits.gardening += cardData.traits.gardening;
        BuildingScript.totalTraits.watering += cardData.traits.watering;
        BuildingScript.totalTraits.mining += cardData.traits.mining;
        BuildingScript.totalTraits.lumber += cardData.traits.lumber;
        BuildingScript.totalTraits.transportation += cardData.traits.transportation;
        BuildingScript.totalTraits.medicine += cardData.traits.medicine;
        BuildingScript.totalTraits.kindling += cardData.traits.kindling;
        BuildingScript.totalTraits.electric += cardData.traits.electric;
        BuildingScript.totalTraits.freezing += cardData.traits.freezing;
    }

    public void RemoveTraitsFromBuilding()
    {
        BuildingScript.totalTraits.handyWork -= cardData.traits.handyWork;
        BuildingScript.totalTraits.foraging -= cardData.traits.foraging;
        BuildingScript.totalTraits.gardening -= cardData.traits.gardening;
        BuildingScript.totalTraits.watering -= cardData.traits.watering;
        BuildingScript.totalTraits.mining -= cardData.traits.mining;
        BuildingScript.totalTraits.lumber -= cardData.traits.lumber;
        BuildingScript.totalTraits.transportation -= cardData.traits.transportation;
        BuildingScript.totalTraits.medicine -= cardData.traits.medicine;
        BuildingScript.totalTraits.kindling -= cardData.traits.kindling;
        BuildingScript.totalTraits.electric -= cardData.traits.electric;
        BuildingScript.totalTraits.freezing -= cardData.traits.freezing;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;

        LargeCardViewScript.Instance.CloseZoom(gameObject);
    }

    private void CheckForAltPress()
    {
        viewButtonPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);

        if((Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt)) && !viewButtonPressed)
        {
            if(!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
                LargeCardViewScript.Instance.CloseZoom(gameObject);
        }
    }
}
