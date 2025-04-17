using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class UnitCardScript : MonoBehaviour
{
    protected Image image;
    protected Button button;
    public CardData cardData;
    public Color normalColor; //Probably Temp
    public Color selectColor;
    [SerializeField] TMP_Text health;
    [HideInInspector] public PhotonView opponentMirror;

    //Effects and state variables
    public bool resting;

    //Coroutine Checks
    private bool readyForNextAttackAction;


    void Awake()
    {
        button = GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
    }

    public void SetUpCard(CardData newData)
    {
        cardData = newData;
        image.sprite = cardData.cardArt;
        cardData.currentHp = cardData.maxHp;
        health.text = cardData.currentHp.ToString();
        opponentMirror.RPC("UpdateHealth", RpcTarget.Others, cardData.currentHp);
    }
    
    public void Hurt(int dmg)
    {
        cardData.currentHp -= dmg;

        if(cardData.currentHp <= 0)
            Die();
        
        health.text = cardData.currentHp.ToString();
        opponentMirror.RPC("UpdateHealth", RpcTarget.Others, cardData.currentHp);
    }

    public void Heal(int heal)
    {
        cardData.currentHp += heal;

        if(cardData.currentHp > cardData.maxHp)
            cardData.currentHp = cardData.maxHp;
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

    public IEnumerator Attack() //Rewire to look at HandScript.Instance.Selected, then add a blocker GameObject that takes the hit if it's not null
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
            
        target.SendMessage("Hurt", cardData.currentAtk);

        if(cardData.OnAttack != null)
        {
            for(int i = 0; i < cardData.OnAttack.Count; i++)
            {
                cardData.OnAttack[i].Invoke();
                yield return new WaitUntil(() => readyForNextAttackAction);
                readyForNextAttackAction = false;
            }
        }

        yield return null;
        HandScript.Instance.currentAttacker = null;
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
        transform.rotation = Quaternion.Euler(0, 0, -90);
        resting = true;
        opponentMirror.RPC("NormalRest", RpcTarget.Others);
    }

    public void Wake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        resting = false;
        opponentMirror.RPC("Wake", RpcTarget.Others);
    }
    
    public void AfterBlockActions()
    {
        if(cardData.traits.tank)
            Wake();
    }
    protected virtual void Die()
    {
        Debug.Log("Unit is now dead :(");
        HandScript.Instance.playerDiscardPile.SendMessage("DiscardCard", cardData);
        opponentMirror.RPC("HeldUnitDeath", RpcTarget.Others);
        Destroy(gameObject);
    }

    public void Deselect()
    {
        image.color = normalColor;
    }
}
