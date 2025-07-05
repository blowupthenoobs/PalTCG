using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

public class TargetingMechanisms: MonoBehaviour
{
    public static void SelectTarget()
    {
        //For hurt and death effects, will set the target to the attacker
    }

    public static void TargetAllEnemies()
    {
        //For hurt and death effects, will set the target to the attacker
    }

    public static void TargetAttacker()
    {
        //For hurt and death effects, will set the target to the attacker
    }
}

public class StatusEffects: MonoBehaviour
{
    private static bool canMoveToNextStep;
    public static void PoisonCard()
    {
        //does nothing rn
    }

    public static IEnumerator PutToSleep()
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

    public static void RestTargets()
    {
        for(int i = 0; i < HandScript.Instance.selection.Count; i++)
        {
            HandScript.Instance.selection[i].SendMessage("SendRestEffect");
            HandScript.Instance.selection[i].SendMessage("Deselect");
        }
    }
}

public class AllowConfirmations
{
    public static void LookForSingleTarget()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count == 1);
    }

    public static void ClearButtonEffects()
    {
        ConfirmationButtons.Instance.Confirmed = null;
        ConfirmationButtons.Instance.Denied = null;
        HandScript.Instance.updateSelection = null;
        GameManager.Instance.HideConfirmationButtons();
    }
}

public class CardEffectCoroutines: MonoBehaviour
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
