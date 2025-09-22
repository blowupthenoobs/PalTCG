using System.Collections;
using System.Linq;
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

    public static void ReturnFieldCardToDeck(GameObject card)
    {
        HandScript.Instance.playerDrawPile.GetComponent<DrawPileScript>().currentDeck.Insert(0, card.GetComponent<UnitCardScript>().cardData);
        card.GetComponent<UnitCardScript>().RemoveFromSphere();
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

        if(HandScript.Instance.blocker == null)
            target = HandScript.Instance.selected;
        else
            target = HandScript.Instance.blocker;

        return target;
    }
}

public class StatusEffectAbilities : MonoBehaviour
{
    private static bool canMoveToNextStep;
    public static void BurnCard(int power = 1)
    {
        GiveTokensToTarget("burning", power);
        AbilityActivation.readyForNextAttackAction = true;
    }

    public static void PoisonCard()
    {
        GiveTokensToTarget("poisoned", 2);
        AbilityActivation.readyForNextAttackAction = true;
    }

    public static void ShockTarget()
    {
        var target = TargetingMechanisms.TargetAttackedEnemy();
        
        target.transform.GetComponent<EnemyPalCardScript>().opponentMirror.RPC("GetShocked", RpcTarget.Others);

        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().opponentMirror.RPC("ShockOtherCard", RpcTarget.Others);

        AbilityActivation.readyForNextAttackAction = true;
    }

    public static void PutToSleep()
    {
        HandScript.Instance.selection.Clear();
        GameManager.Instance.ShowConfirmationButtons("select target to rest?");
        HandScript.Instance.state = "settingAilment";
        HandScript.Instance.updateSelection = AllowConfirmations.LookForSingleTarget;
        ConfirmationButtons.Instance.Confirmed += RestTargets;
        ConfirmationButtons.Instance.Confirmed += () => AbilityActivation.readyForNextAttackAction = true;
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "choosingAttack";
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => AbilityActivation.readyForNextAttackAction = true;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "choosingAttack";
        ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
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

    public static void HaveSelectedCard()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selected != null);
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

        GameManager.Instance.ShowConfirmationButtons("equip pal as " + slotType + "(will remove current " + slotType +")");
        HandScript.Instance.state = "awaitingDecision";
        ConfirmationButtons.Instance.Confirmed += () => cardToMove.SendMessage("ActivatePalSkill");
        ConfirmationButtons.Instance.Confirmed += () => MoveToItemSlot(cardToMove, slotType);
        ConfirmationButtons.Instance.Confirmed += () => cardToMove.SendMessage("UseAbility");
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => cardToMove.SendMessage("UseAbility");
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
}

    public static void MoveToItemSlot(GameObject cardToMove, string slotType)
    {
        cardToMove.transform.parent.gameObject.SendMessage("PrepareCardForMoving");
        ToolSlotScript.ForceEquipCardToCorrectSlot(cardToMove, slotType);
    }
}

public class AbilityActivation : MonoBehaviour
{
    public static bool readyForNextAttackAction;
    public static IEnumerator RunWhenAttackAbilities(List<string> traitList)
    {
        yield return null;

        if (HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().statuses.poisoned == 0)
        {
            if (traitList.Contains("daedream"))
            {
                for (int i = 0; i < traitList.Count(n => n == "daedream"); i++)
                {
                    StatusEffectAbilities.PutToSleep();
                    yield return new WaitUntil(() => readyForNextAttackAction);
                }
            }
        }

        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect();
    }

    public static IEnumerator RunOnAttackAbilities(List<string> traitList)
    {
        yield return null;
        if (HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().statuses.poisoned == 0)
        {
            if (traitList.Contains("burn"))
                StatusEffectAbilities.BurnCard(traitList.Count(n => n == "burn"));
            if (traitList.Contains("toxic"))
                StatusEffectAbilities.PoisonCard();
            if (traitList.Contains("stun"))
                StatusEffectAbilities.ShockTarget();
        }


        HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect();
    }

    public static IEnumerator UsePalSkills(string palSkill)
    {
        yield return null;

        if (new[] { "lamball", "foxsparks" }.Contains(palSkill))
        {
            CardMovement.EquipAsItemPalSkill("weapon");
            yield break;
        }

    }
}

public class SpecificPalAbilities : MonoBehaviour
{
    public static void SelectFriendlyForChikipiAbility()
    {
        HandScript.Instance.selection.Clear();
        GameManager.Instance.ShowConfirmationButtons("select friendly to use effect on");
        HandScript.Instance.state = "selectingEffectForFriendly";

        HandScript.Instance.updateSelection = AllowConfirmations.LookForSingleTarget;
        ConfirmationButtons.Instance.Confirmed += SelectChikipiAbilityTarget;
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "endOfTurnAbilities";
        ConfirmationButtons.Instance.Denied += HandScript.Instance.selected.GetComponent<UnitCardScript>().FinishEffect;
        ConfirmationButtons.Instance.Denied += HandScript.Instance.selection.Clear;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
    }

    public static void SelectChikipiAbilityTarget()
    {
        GameObject selectedAlly = HandScript.Instance.selection[0];
        GameManager.Instance.ShowConfirmationButtons("select enemy to target");
        HandScript.Instance.state = "settingAilment";

        HandScript.Instance.updateSelection = AllowConfirmations.LookForSingleTarget;
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "endOfTurnAbilities";
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.selection[0].GetComponent<EnemyPalCardScript>().opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, selectedAlly.GetComponent<UnitCardScript>().cardData.currentAtk, false);
        ConfirmationButtons.Instance.Confirmed += () => HandFunctions.ReturnFieldCardToDeck(selectedAlly); //This is where you make the thing go back into the deck
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.selection.Clear();
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "endOfTurnAbilities";
        ConfirmationButtons.Instance.Denied += HandScript.Instance.selected.GetComponent<UnitCardScript>().FinishEffect;
        ConfirmationButtons.Instance.Denied += HandScript.Instance.selection.Clear;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
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
