using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

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

    public readonly static Dictionary<string, UnityAction> turnEndAbilities =  new Dictionary<string, UnityAction>
    {
        { "chikipi", () => Debug.Log("Used Chikipi Ability")},
    };

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerCard = gameObject;
        enemyHandler = gameObject.GetComponent<PhotonView>();
    }

    [PunRPC]
    public void LookForBlockers()
    {
        if(CheckBoardForTags("blocker", true) > 0)
        {
            ChooseBlocker();
        }
        else
            HandScript.Instance.opponentMirror.RPC("PassAction", RpcTarget.Others);
    }

    public void ChooseBlocker()
    {
        HandScript.Instance.state = "blocking";
        GameManager.Instance.ShowConfirmationButtons();
        HandScript.Instance.updateSelection += AllowConfirmations.LookForSingleTarget;
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

    public int CheckBoardForTags(string tag, bool cannotRest = false)
    {
        int count = 0;
        foreach (GameObject space in cardSlots)
        {
            if(space.GetComponent<PalSphereScript>().heldCard != null)
            {
                if((!space.GetComponent<PalSphereScript>().heldCard.GetComponent<UnitCardScript>().resting || !cannotRest) && space.GetComponent<PalSphereScript>().heldCard.GetComponent<UnitCardScript>().CanBlock())
                    count++;
            }
        }

        return count;
    }

    public bool BoardHasEndOfTurnAbilities()
    {
        foreach(GameObject space in cardSlots)
        {
            if(space.GetComponent<CardHolderScript>().heldCard != null)
            {
                if(space.GetComponent<CardHolderScript>().heldCard.GetComponent<UnitCardScript>().CheckForUsuableTraits(turnEndAbilities.Keys.ToList()))
                    return true;
            }
        }

        return false;
    }

    public void RunEndOfTurnAbilities()
    {
        if(BoardHasEndOfTurnAbilities())
        {
            foreach(GameObject space in cardSlots)
            {
                space.GetComponent<CardHolderScript>().heldCard?.GetComponent<UnitCardScript>().PrepareEndPhase();
            }

            HandScript.Instance.state = "endOfTurnAbilities";
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.updateSelection += AllowConfirmations.LookForSingleTarget;
            ConfirmationButtons.Instance.Confirmed += () => StartCoroutine(HandScript.Instance.selected.GetComponent<UnitCardScript>().RunThroughTurnEndAbilities());
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += () => GameManager.Instance.readyForNextAttackAction = true;
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

            HandScript.Instance.updateSelection.Invoke();
        }
        else
            GameManager.Instance.readyForNextAttackAction = true;
        
    }
}
