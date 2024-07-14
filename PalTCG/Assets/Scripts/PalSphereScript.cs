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
                Debug.Log("asking");
                HandScript.Instance.buildingPay = true;
                ConfirmationButtons.Instance.Confirmed += PlaceCard;
                ConfirmationButtons.Instance.Denied += Disengage;
            }
        }
    }

    void PlaceCard()
    {
        ConfirmationButtons.Instance.Confirmed -= PlaceCard;

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
        ConfirmationButtons.Instance.Confirmed -= PlaceCard;
        ConfirmationButtons.Instance.Confirmed -= Disengage;

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.buildingPay = false;
    }
}
