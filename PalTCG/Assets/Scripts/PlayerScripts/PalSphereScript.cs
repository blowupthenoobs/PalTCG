using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphereScript : MonoBehaviour
{
    [SerializeField] GameObject waitingSpace;
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

    public void CheckForCard()
    {
        if(HandScript.Instance.selected != null && heldCard == null && HandScript.Instance.state == "default")
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                GameManager.Instance.ShowConfirmationButtons();
                HandScript.Instance.state = "buildingPay";
                HandScript.Instance.Raise();
                HandScript.Instance.updateSelection += VerifyButtons;
                ConfirmationButtons.Instance.Confirmed += PayForCard;
                ConfirmationButtons.Instance.Denied += Disengage;

                VerifyButtons();
            }
        }
        else if(HandScript.Instance.state == "lookingForSphere")
        {
            HandScript.Instance.Select(gameObject);
        }
    }

    void PlaceCard(GameObject card)
    {
        heldCard = card;
        heldCard.transform.SetParent(transform);
        heldCard.transform.position = transform.position;
    }

    void PayForCard()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PayForCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        GameManager.Instance.HideConfirmationButtons();

        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;

        if(data.size <= 1)
        {
            heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
            
            PlaceCard(heldCard);

            heldCard.GetComponent<PalCardScript>().cardData = data;
        }
        else
        {
            waitingSpace.SendMessage("AddToWaitlist", data);
        }
        


        HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
        Destroy(HandScript.Instance.selected);

        while(HandScript.Instance.selection.Count > 0)
        {
            var cardToDiscard = HandScript.Instance.selection[0];
            HandScript.Instance.selection.RemoveAt(0);
            HandScript.Instance.Discard(cardToDiscard);
        }  

        HandScript.Instance.state = "default";
    }

    void Disengage()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PayForCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.state = "default";
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(Resources.PaymentIsCorrect());
    }

}
