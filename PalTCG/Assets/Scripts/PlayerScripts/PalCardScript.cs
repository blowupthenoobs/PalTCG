using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalCardScript : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Resources.StatusEffects statuses;
    public CardData cardData;

    void Awake()
    {
        button = GetComponent<Button>();
    }
    
    public void PlaceOnPalSphere()
    {
        var palSphere = HandScript.Instance.selection[0];

        palSphere.SendMessage("PlaceCard", gameObject);
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

}
