using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;

public class PalSphereScript : MonoBehaviour
{
    [SerializeField] GameObject waitingSpace;
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

    public void CheckForCard()
    {
        if(GameManager.Instance.phase == "PlayerTurn")
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
                    ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;

                    VerifyButtons();
                }
            }
            else if(HandScript.Instance.state == "lookingForSphere")
            {
                HandScript.Instance.Select(gameObject);
            }
        }
        else if(HandScript.Instance.selected != null)
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                HandScript.Instance.selected = null;
            }
        }
    }

    void PlaceCard(GameObject card)
    {
        heldCard = card;
        heldCard.transform.SetParent(transform);
        heldCard.transform.position = transform.position;

        GameManager.Instance.StartPlayerAttack += heldCard.GetComponent<PalCardScript>().PrepareAttackPhase;
        GameManager.Instance.StartPlayerTurn += heldCard.GetComponent<PalCardScript>().Wake;
        GameManager.Instance.StartEnemyTurn += heldCard.GetComponent<PalCardScript>().EndAttackPhase;
        GameManager.Instance.StartEnemyTurn += heldCard.GetComponent<PalCardScript>().Wake;
    }

    void PayForCard()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PayForCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        ConfirmationButtons.Instance.Denied -= HandScript.Instance.ClearSelection;
        GameManager.Instance.HideConfirmationButtons();

        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;

        if(data.size <= 1)
        {
            heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
            PlaceCard(heldCard);

            heldCard.SendMessage("SetUpCard", data);
        }
        else
        {
            waitingSpace.SendMessage("AddToWaitlist", data);
        }
        


        HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
        Destroy(HandScript.Instance.selected);
        HandScript.Instance.selected = null;

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
        ConfirmationButtons.Instance.Denied -= HandScript.Instance.ClearSelection;
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.state = "default";
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(ResourceProcesses.PalPaymentIsCorrect());
    }

}
