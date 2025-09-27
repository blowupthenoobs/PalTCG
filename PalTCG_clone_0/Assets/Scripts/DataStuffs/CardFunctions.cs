using System.Collections;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Resources;
public class HandFunctions : MonoBehaviour
{
    public static void SelectRandomCards(int amount)
    {
        HandScript.Instance.UnselectSelection();

        while(HandScript.Instance.selection.Count < amount || HandScript.Instance.selection.Count == HandScript.Instance.Hand.Count)
        {
            int random = Random.Range(0, HandScript.Instance.Hand.Count);
            if(!HandScript.Instance.selection.Contains(HandScript.Instance.Hand[random]))
                HandScript.Instance.selection.Add(HandScript.Instance.Hand[random]);
        }
    }

    public static void SendSelectionToDiscard()
    {
        while (HandScript.Instance.selection.Count > 0)
        {
            var cardToDiscard = HandScript.Instance.selection[0];
            HandScript.Instance.selection.RemoveAt(0);
            HandScript.Instance.Discard(cardToDiscard);
        }
    }

    public static void DiscardCards(int amount = 1)
    {
        GameManager.Instance.ShowConfirmationButtons("Discard " + amount + " card(s)", false);
        HandScript.Instance.state = "buildingPay";
        HandScript.Instance.updateSelection = () => ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count == amount || HandScript.Instance.selection.Count == HandScript.Instance.Hand.Count);
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Confirmed += SendSelectionToDiscard;
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => SelectRandomCards(amount);
        ConfirmationButtons.Instance.Denied += SendSelectionToDiscard;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

        ConfirmationButtons.Instance.AllowConfirmation(true);
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

    public static void DrawCards(int amount)
    {
        while(amount > 0)
        {
            HandScript.Instance.playerDrawPile.SendMessage("Draw");
            amount--;
        }
    }

    public static void ReturnFieldCardToDeck(GameObject card)
    {
        HandScript.Instance.playerDrawPile.GetComponent<DrawPileScript>().currentDeck.Insert(0, card.GetComponent<UnitCardScript>().cardData);
        card.GetComponent<UnitCardScript>().RemoveFromSphere();
    }

    public static void PlayCardFromPalBox()
    {
        if (HandScript.Instance.tempDataTypeRef is PalCardData cardData)
        {
            FieldAbilityHandlerScript.Instance.waitingSpace.GetComponent<WaitingSpace>().PlaceCard(cardData);
            HandScript.Instance.playerDiscardPile.SendMessage("RemoveCard", cardData);
        }
    }

    public static void PayForDiscardedPalCard(int alteredCost = 0)
    {
        GameManager.Instance.ShowConfirmationButtons("select cards for payment");
        HandScript.Instance.state = "buildingPay";
        HandScript.Instance.Raise();
        HandScript.Instance.updateSelection = () => AllowConfirmations.CanPayForPalCard((PalCardData)HandScript.Instance.tempDataTypeRef, alteredCost);
        ConfirmationButtons.Instance.Confirmed += PlayCardFromPalBox;
        ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

        AllowConfirmations.CanPayForPalCard((PalCardData)HandScript.Instance.tempDataTypeRef, alteredCost);
    }

    public static void ChikipiPalSkill()
    {
        GameObject chikipiCard = FieldCardContextMenuScript.Instance.activeCard;
        GameManager.Instance.ShowConfirmationButtons("select palcard from discard to play");
        HandScript.Instance.state = "choosingCardInDiscard";
        // HandScript.Instance.Raise(); //Make it open discard pile instead
        HandScript.Instance.updateSelection = () => ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.tempDataTypeRef != null && HandScript.Instance.tempDataTypeRef is PalCardData);
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Confirmed += () => PayForDiscardedPalCard(-1);
        ConfirmationButtons.Instance.Confirmed += () => AbilityActivation.TrackActionsForPalSkill(chikipiCard, 1);
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.tempDataTypeRef != null && HandScript.Instance.tempDataTypeRef is PalCardData);
        HandScript.Instance.playerDiscardPile.SendMessage("Click");
    }

    public static void CattivaPalSkill()
    {
        GameObject cattivaCard = FieldCardContextMenuScript.Instance.activeCard;
        GameManager.Instance.ShowConfirmationButtons("Use Cattiva Palskill?");
        HandScript.Instance.state = "awaitingDecision";
        ConfirmationButtons.Instance.Confirmed += () => DrawCards(2);
        ConfirmationButtons.Instance.Confirmed += () => cattivaCard.SendMessage("ActivatePalSkill");
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Confirmed += () => DiscardCards(1);
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "default";
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

        ConfirmationButtons.Instance.AllowConfirmation(true);
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
        GiveMinStatusToTarget("poisoned", 2);
        AbilityActivation.readyForNextAttackAction = true;
    }

    public static void ShockTarget()
    {
        var target = TargetingMechanisms.TargetAttackedEnemy();
        
        target.transform.GetComponent<EnemyPlayerScript>()?.opponentMirror.RPC("GetShocked", RpcTarget.Others);
        target.transform.GetComponent<EnemyPalCardScript>()?.opponentMirror.RPC("GetShocked", RpcTarget.Others);

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

    public static void CanPayForPalCard(int alteredCost = 0)
    {
        ConfirmationButtons.Instance.AllowConfirmation(ResourceProcesses.PalPaymentIsCorrect(alteredCost));
    }

    public static void CanPayForPalCard(PalCardData data, int alteredCost = 0)
    {
        ConfirmationButtons.Instance.AllowConfirmation(ResourceProcesses.PalPaymentIsCorrect(data, alteredCost));
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

        if(new[] { "lamball", "foxsparks" }.Contains(palSkill))
        {
            CardMovement.EquipAsItemPalSkill("weapon");
            yield break;
        }
        if(palSkill == "chikipi")
        {
            HandFunctions.ChikipiPalSkill();
        }
    }

    public static void TrackActionsForPalSkill(GameObject card, int functionCount)
    {
        if(functionCount == 0)
            card.SendMessage("ActivatePalSkill");
        else
            ConfirmationButtons.Instance.Confirmed += () => TrackActionsForPalSkill(card, functionCount - 1);
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
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Confirmed += SelectChikipiAbilityTarget;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "endOfTurnAbilities";
        ConfirmationButtons.Instance.Denied += HandScript.Instance.selected.GetComponent<UnitCardScript>().FinishEffect;
        ConfirmationButtons.Instance.Denied += HandScript.Instance.UnselectSelection;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.selected.SendMessage("DontUseAbility");
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
    }

    public static void SelectChikipiAbilityTarget()
    {
        GameObject selectedAlly = HandScript.Instance.selection[0];
        HandScript.Instance.selection.Clear();
        GameManager.Instance.ShowConfirmationButtons("select enemy to target");
        HandScript.Instance.state = "settingAilment";

        HandScript.Instance.updateSelection = AllowConfirmations.LookForSingleTarget;
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.state = "endOfTurnAbilities";
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.selection[0].GetComponent<EnemyPlayerScript>()?.opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, selectedAlly.GetComponent<UnitCardScript>().cardData.currentAtk, false);
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.selection[0].GetComponent<EnemyPalCardScript>()?.opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, selectedAlly.GetComponent<UnitCardScript>().cardData.currentAtk, false);
        ConfirmationButtons.Instance.Confirmed += () => HandFunctions.ReturnFieldCardToDeck(selectedAlly); //This is where you make the thing go back into the deck
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.UnselectSelection();
        ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.selected.SendMessage("UseAbility");
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.state = "endOfTurnAbilities";
        ConfirmationButtons.Instance.Denied += HandScript.Instance.selected.GetComponent<UnitCardScript>().FinishEffect;
        ConfirmationButtons.Instance.Denied += HandScript.Instance.UnselectSelection;
        ConfirmationButtons.Instance.Denied += () => selectedAlly.SendMessage("Deselect");
        ConfirmationButtons.Instance.Denied += () => HandScript.Instance.selected.SendMessage("DontUseAbility");
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
