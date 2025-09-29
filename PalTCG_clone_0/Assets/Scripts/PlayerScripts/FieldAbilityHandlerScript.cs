using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

using Resources;
public class FieldAbilityHandlerScript : MonoBehaviour
{
    public static FieldAbilityHandlerScript Instance;
    public PhotonView opponentMirror;
    public PhotonView enemyHandler;

    //Board References
    [HideInInspector] public GameObject playerCard;
    public List<GameObject> cardSlots = new List<GameObject>();
    public GameObject playerDrawPile;
    public GameObject playerDiscardPile;
    public GameObject waitingSpace;

    public List<string> fieldPalEffects = new List<string>();

    public bool stalling;
    private bool actionPassedThrough;
    private bool actionRejected;

    public readonly static Dictionary<string, UnityAction> turnEndAbilities = new Dictionary<string, UnityAction>
    {
        { "chikipi", () => SpecificPalAbilities.SelectFriendlyForChikipiAbility()},
        { "incineram", () => StatusEffectAbilities.IncineramActiveAbility()},
    };

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerCard = gameObject;
        enemyHandler = gameObject.GetComponent<PhotonView>();
        GameManager.Instance.EndEnemyTurn += TurnOffPalSkills;
    }

    [PunRPC]
    public void LookForBlockers()
    {
        if (CheckBoardForAvailableBlockers("blocker", true) > 0)
        {
            ChooseBlocker();
        }
        else
            HandScript.Instance.opponentMirror.RPC("PassAction", RpcTarget.Others);
    }

    public void ChooseBlocker()
    {
        HandScript.Instance.state = "blocking";
        GameManager.Instance.ShowConfirmationButtons("Select blocker?");
        HandScript.Instance.updateSelection = AllowConfirmations.LookForSingleTarget;
        ConfirmationButtons.Instance.Confirmed += SetBlocker;
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.opponentMirror.RPC("PassAction", RpcTarget.Others);
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "";
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.opponentMirror.RPC("PassAction", RpcTarget.Others);
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "";
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

        HandScript.Instance.updateSelection.Invoke();
    }

    private void SetBlocker()
    {
        HandScript.Instance.selection[0].GetComponent<UnitCardScript>().opponentMirror.RPC("Block", RpcTarget.Others);
    }

    public int CheckBoardForAvailableBlockers(string tag, bool cannotRest = false)
    {
        int count = 0;
        foreach (GameObject space in cardSlots)
        {
            if (space.GetComponent<PalSphereScript>().heldCard != null)
            {
                if (!space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().resting && space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().CanBlock())
                    count++;
            }
        }

        return count;
    }

    public int CheckTagCountOnBoard(string tag, bool cannotRest = false)
    {
        int count = 0;
        foreach (GameObject space in cardSlots)
        {
            if (space.GetComponent<PalSphereScript>().heldCard != null)
            {
                if ((!space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().resting || !cannotRest) && space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().cardData.traits.tags.Contains(tag))
                    count += space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().cardData.traits.tags.Count(x => x == tag);
            }
        }

        return count;
    }

#region broadAbilityTriggers

    public bool BoardHasEndOfTurnAbilities()
    {
        foreach (GameObject space in cardSlots)
        {
            if (space.GetComponent<CardHolderScript>().heldCard != null)
            {
                if (space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().CheckForUsuableTraits(turnEndAbilities.Keys.ToList()))
                    return true;
            }
        }

        return false;
    }

    public void RunEndOfTurnAbilities()
    {
        if (BoardHasEndOfTurnAbilities())
        {
            foreach (GameObject space in cardSlots)
            {
                if (space.GetComponent<CardHolderScript>().heldCard != null)
                    space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().PrepareEndPhase();
            }

            string previousState = HandScript.Instance.state;
            HandScript.Instance.state = "endOfTurnAbilities";
            GameManager.Instance.ShowConfirmationButtons("select end of turn ability?");
            HandScript.Instance.updateSelection = AllowConfirmations.HaveSelectedCard;
            ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = previousState;
            ConfirmationButtons.Instance.Confirmed += () => StartCoroutine(HandScript.Instance.selected.GetComponent<UnitCardScript>().RunThroughTurnEndAbilities());
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += () => GameManager.Instance.readyForNextAttackAction = true;
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

            HandScript.Instance.updateSelection.Invoke();
        }
        else
            GameManager.Instance.readyForNextAttackAction = true;

    }

    public int ExtraAbilityAttackDamage(List<string> tags)
    {
        int extraDamage = 0;

        if (tags.Contains("burn"))
            extraDamage += fieldPalEffects.Count(x => x == "rooby");

        return extraDamage;
    }

    public static bool CanUseSpecificPalSkill(GameObject card)
    {
        var palSkill = ((PalCardData)card.GetComponent<PalCardScript>().cardData).palSkill;
        if (new List<string> { "lamball", "firesparks" }.Contains(palSkill))
            return !(card == ToolSlotScript.GetHeldItemOnSlot("weapon"));

        return true;
    }

    public void TurnOffPalSkills()
    {
        fieldPalEffects.Clear();
    }

    public void AbilityPassedThrough()
    {
        actionPassedThrough = true;
    }

    public void AbilityRejected()
    {
        actionRejected = true;
    }
#endregion broadAbilityTriggers

#region miscSpecificAbilities

    [PunRPC]
    public void BurnTriggered()
    {
        stalling = true;
        StartCoroutine(RunOnBurningEffects());
    }

    private IEnumerator RunOnBurningEffects()
    {
        if (CheckTagCountOnBoard("rooby") >= 0)
        {
            for (int i = 0; i < CheckTagCountOnBoard("rooby"); i++)
            {
                HandFunctions.RoobyAbility();
                yield return new WaitUntil(() => actionPassedThrough || actionRejected);

                if(actionRejected)
                {
                    Debug.Log("broken");
                    actionRejected = false;
                    break;
                }

                actionPassedThrough = false;
            }
        }

        stalling = false;
    }
#endregion
}
