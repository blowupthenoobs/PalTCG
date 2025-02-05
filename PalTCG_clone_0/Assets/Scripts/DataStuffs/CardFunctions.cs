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
        ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
        ConfirmationButtons.Instance.Denied += HandScript.Instance.currentAttacker.GetComponent<UnitCardScript>().FinishEffect;
        ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
        //ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
        //equip trigger to finish effect

        //put target to sleep
    }

    public static void RestTargets()
    {
        for(int i = 0; i < HandScript.Instance.selection.Count; i++)
        {
            HandScript.Instance.selection[i].transform.parent.SendMessage("SendRestEffect"); //Will have to switch to telling server to tell it to rest
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
        GameManager.Instance.HideConfirmationButtons();
        HandScript.Instance.state = "choosingAttack";
    }
}

public class CardEffectCoroutines: MonoBehaviour
{
    
}

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;

    public static CoroutineHelper Instance
    {
        get
        {
            if (_instance == null)
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
