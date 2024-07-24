using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphereScript : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

    public void CheckForCard()
    {
        if(HandScript.Instance.selected != null && heldCard == null && !HandScript.Instance.buildingPay)
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                GameManager.Instance.ShowConfirmationButtons();
                HandScript.Instance.buildingPay = true;
                HandScript.Instance.Raise();
                HandScript.Instance.updatePayment += VerifyButtons;
                ConfirmationButtons.Instance.Confirmed += PlaceCard;
                ConfirmationButtons.Instance.Denied += Disengage;

                VerifyButtons();
            }
        }
    }

    void PlaceCard()
    {
        HandScript.Instance.updatePayment -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PlaceCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        GameManager.Instance.HideConfirmationButtons();

        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;

        heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard.transform.SetParent(transform);

        heldCard.GetComponent<PalCardScript>().cardData = data;

        HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
        Destroy(HandScript.Instance.selected);

        while(HandScript.Instance.payment.Count > 0)
        {
            var cardToDiscard = HandScript.Instance.payment[0];
            HandScript.Instance.payment.RemoveAt(0);
            HandScript.Instance.Discard(cardToDiscard);
        }  

        HandScript.Instance.buildingPay = false;
    }

    void Disengage()
    {
        HandScript.Instance.updatePayment -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PlaceCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        GameManager.Instance.ShowConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.buildingPay = false;
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(PaymentIsCorrect());
    }

    bool PaymentIsCorrect()
    {
        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;
        var costAmount = data.cost;

        if(data.element == Resources.Element.Basic && HandScript.Instance.payment.Count == costAmount)
            return true;
        else
            return false;
    }
}
