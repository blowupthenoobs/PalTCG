using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PalCardScript : UnitCardScript
{
    [SerializeField] Resources.StatusEffects statuses;

    public void PlaceOnPalSphere()
    {
        var palSphere = HandScript.Instance.selection[0];

        palSphere.SendMessage("PlaceCard", gameObject);
        GameManager.Instance.HideConfirmationButtons();
    }
    
    public void ReadyToBePlaced()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LookForPalSphere);
    }

    void LookForPalSphere()
    {
        if(HandScript.Instance.state == "default")
        {
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.state = "lookingForSphere";
            HandScript.Instance.Duck();
            HandScript.Instance.updateSelection += VerifyButtons;                
            ConfirmationButtons.Instance.Confirmed += PlaceOnPalSphere;
            ConfirmationButtons.Instance.Denied += StopLookingForSphere;

            VerifyButtons();
        }
    }

    void StopLookingForSphere()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PlaceOnPalSphere;
        ConfirmationButtons.Instance.Denied -= StopLookingForSphere;
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.state = "default";
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(SphereSelected());
    }

    bool SphereSelected()
    {
        if(HandScript.Instance.selection.Count == 1)
            return true;
        else
            return false;
    }

    protected override void Die()
    {
        transform.parent.gameObject.SendMessage("BreakSphere");
        base.Die();
    }

}
