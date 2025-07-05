using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;
public class ToolCardScript : UnitCardScript
{

    public void PlaceOnToolSlot()
    {
        var toolSlot = HandScript.Instance.selection[0];

        toolSlot.SendMessage("PlaceOnCorrectSpot", gameObject);
        HandScript.Instance.state = "default";
        GameManager.Instance.HideConfirmationButtons();
    }

    public void ReadyToBePlaced()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LookForToolSlot);
    }

    void LookForToolSlot()
    {
        if (HandScript.Instance.state == "default")
        {
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.state = "lookingForSlot";
            HandScript.Instance.Duck();
            HandScript.Instance.updateSelection += VerifyButtons;
            ConfirmationButtons.Instance.Confirmed += PlaceOnToolSlot;
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += StopLookingForSlot;
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

            VerifyButtons();
        }
    }

    void StopLookingForSlot()
    {
        HandScript.Instance.state = "default";
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(SphereSelected());
    }

    bool SphereSelected()
    {
        if (HandScript.Instance.selection.Count == 1)
            return true;
        else
            return false;
    }

}
