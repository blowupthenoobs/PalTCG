using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalCardScript : UnitCardScript
{
    [SerializeField] Resources.StatusEffects statuses;

    public void PlaceOnPalSphere()
    {
        var palSphere = HandScript.Instance.selection[0];

        palSphere.SendMessage("PlaceCard", gameObject);
        GameManager.Instance.HideConfirmationButtons();
    }

    public void PrepareAttackPhase()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(SelectForAttack);
    }

    public void EndAttackPhase()
    {
        button.onClick.RemoveAllListeners();
    }

    public void ReadyToBePlaced()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(LookForPalSphere);
    }

    public void SelectForAttack()
    {
        if(HandScript.Instance.state == "choosingAttack" && HandScript.Instance.selected != gameObject && !HandScript.Instance.selection.Contains(gameObject))
        {
            image.color = selectColor;
            HandScript.Instance.Select(gameObject);
        }
        else
            Deselect();
    }

    public void Deselect()
    {
        if(HandScript.Instance.selected == gameObject)
            HandScript.Instance.selected = null;
        if(HandScript.Instance.selection.Contains(gameObject))
            HandScript.Instance.selection.Remove(gameObject);

        image.color = normalColor;
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

}
