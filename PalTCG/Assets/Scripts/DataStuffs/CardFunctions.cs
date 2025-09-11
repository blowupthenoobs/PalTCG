using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Resources;
public class HandFunctions : MonoBehaviour
{
    public static void SendToDiscard()
    {
        //I think you'll get this one
    }

    public static void SendToHand()
    {
        //This one too
    }

    public static void ReturnToDeck()
    {
        //This one as well
    }

    public static void ShuffleDeck()
    {
        //I think I can stop now
    }

    public static void ChooseTargets()
    {
        //This one will be used to tell the game which targets to ue an ability on
    }
}

public class TargetingMechanisms : MonoBehaviour
{
    public static void SelectTarget()
    {
        //
    }

    public static void TargetAllEnemies()
    {
        //
    }

    public static void TargetAttacker()
    {
        //For hurt and death effects, will set the target to the attacker
    }

    public static GameObject TargetAttackedEnemy()
    {
        GameObject target = null;

        if (HandScript.Instance.blocker == null)
            target = HandScript.Instance.selected;
        else
            target = HandScript.Instance.blocker;

        return target;
    }
}

public class StatusEffectAbilities : MonoBehaviour
{
    private static bool canMoveToNextStep;
    public static void BurnCard()
    {
        GiveTokensToTarget("burning", 1);
        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect();
    }
    public static void PoisonCard()
    {
        GiveTokensToTarget("poisoned", 2);
        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect();
    }

    public static void ShockTarget()
    {
        var target = TargetingMechanisms.TargetAttackedEnemy();
        
        if(target.transform.parent.GetComponent<EnemyPalSphereScript>() != null)
            target.transform.parent.GetComponent<EnemyPalSphereScript>().opponentMirror.RPC("GetShocked", RpcTarget.Others);

        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().opponentMirror.RPC("ShockOtherCard", RpcTarget.Others);

        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect();
    }

    public static IEnumerator PutToSleep()
    {
        if (HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().statuses.poisoned == 0)
        {
            HandScript.Instance.selection.Clear();
            yield return null;
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.state = "settingAilment";
            HandScript.Instance.updateSelection += AllowConfirmations.LookForSingleTarget;
            ConfirmationButtons.Instance.Confirmed += RestTargets;
            ConfirmationButtons.Instance.Confirmed += HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect;
            ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "choosingAttack";
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect;
            ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "choosingAttack";
            ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
        }
        else
        {
            HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect();
        }
    }

    public static void RestTargets()
    {
        for (int i = 0; i < HandScript.Instance.selection.Count; i++)
        {
            HandScript.Instance.selection[i].SendMessage("SendRestEffect");
            HandScript.Instance.selection[i].SendMessage("Deselect");
        }
    }

    public static void GiveTokensToTarget(string tokenType, int tokenCount)
    {

        GameObject target = TargetingMechanisms.TargetAttackedEnemy();
        
        if(target.transform.parent.GetComponent<EnemyPalSphereScript>() != null)
            target.transform.parent.GetComponent<EnemyPalSphereScript>().opponentMirror.RPC("GainTokens", RpcTarget.Others, tokenType, tokenCount);
        //Give the token to targeted enemy
    }
    
    public static void GiveMinStatusToTarget(string tokenType, int tokenCount)
    {

        GameObject target = TargetingMechanisms.TargetAttackedEnemy();
        
        if(target.transform.parent.GetComponent<EnemyPalSphereScript>() != null)
            target.transform.parent.GetComponent<EnemyPalSphereScript>().opponentMirror.RPC("GainTokens", RpcTarget.Others, tokenType, tokenCount);
        //Give the token to targeted enemy
    }
}

public class AllowConfirmations
{
    public static void LookForSingleTarget()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count == 1);
    }

    public static void ResetState()
    {
        HandScript.Instance.state = "default";
    }

    public static void ClearButtonEffects()
    {
        ConfirmationButtons.Instance.Confirmed = null;
        ConfirmationButtons.Instance.Denied = null;
        HandScript.Instance.updateSelection = null;
        GameManager.Instance.HideConfirmationButtons();
    }
}

public class CardMovement : MonoBehaviour
{
    public static void EquipAsItemPalSkill(string slotType)
    {
        GameObject cardToMove = FieldCardContextMenuScript.Instance.activeCard;

        HandScript.Instance.state = "awaitingDecision";
        ConfirmationButtons.Instance.Confirmed += () => cardToMove.SendMessage("ActivatePalSkill");
        ConfirmationButtons.Instance.Confirmed += () => MoveToItemSlot(cardToMove, slotType);
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
}

    public static void MoveToItemSlot(GameObject cardToMove, string slotType)
    {
        ToolSlotScript.ForceEquipCardToCorrectSlot(cardToMove, slotType);
    }
}

public class CardEffectCoroutines : MonoBehaviour
{

}

public class BuildingFunctions : MonoBehaviour
{
    static bool canUseMiningPit;
    static bool canUseLumberFarm;

    public static void RefreshBuildingUses()
    {
        CraftingMenuScript.Instance.RefreshTraitUses();
        canUseMiningPit = true;
        canUseLumberFarm = true;
    }

    public static void OpenCraftingBenchMenu()
    {
        CraftingMenuScript.Instance.OpenCraftingMenu(0);
    }

    public static void UseMiningPit()
    {
        if(canUseMiningPit && HandScript.Instance.state == "default")
        {
            canUseMiningPit = false;
            HandScript.Instance.GatheredItems.stone += BuildingScript.totalTraits.mining;
        }
        else
            Debug.Log("Already used this turn");
    }

    public static void UseLumberFarm()
    {
        if(canUseLumberFarm && HandScript.Instance.state == "default")
        {
            canUseLumberFarm = false;
            HandScript.Instance.GatheredItems.wood += BuildingScript.totalTraits.lumber;
        }
        else
            Debug.Log("Already used this turn");
    }
}

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;

    public static CoroutineHelper Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject helperObject = new GameObject("CoroutineHelper");
                _instance = helperObject.AddComponent<CoroutineHelper>();
            }
            return _instance;
        }
    }

    // This method allows calling coroutines from static methods
    public void StartHelperCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
