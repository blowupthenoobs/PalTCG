using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Resources;
public class ToolCardScript : UnitCardScript
{
    [HideInInspector] public GameObject waitingSpace;
    [SerializeField] GameObject palCardPrefab;
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

    private void LookForToolSlot()
    {
        if(HandScript.Instance.state == "default")
        {
            GameManager.Instance.ShowConfirmationButtons("place on tool slot?");
            HandScript.Instance.state = "lookingForSlot";
            HandScript.Instance.Duck();
            HandScript.Instance.updateSelection = VerifyButtons;
            ConfirmationButtons.Instance.Confirmed += PlaceOnToolSlot;
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += StopLookingForSlot;
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;

            VerifyButtons();
        }
    }

    private void StopLookingForSlot()
    {
        HandScript.Instance.state = "default";
    }

    private void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(SphereSelected());
    }

    private bool SphereSelected()
    {
        if(HandScript.Instance.selection.Count == 1)
            return true;
        else
            return false;
    }

    public void PrepareMainPhase()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(MainPhaseClick);
    }

    private void MainPhaseClick()
    {
        if(HandScript.Instance.state == "default")
        {
            if(HandScript.Instance.selected != null)
            {
                if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
                {
                    if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is ToolCardData)
                    {
                        Debug.Log("called");
                        transform.parent.SendMessage("CheckForCard");
                    }
                    else if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is PalCardData && ((ToolCardData)cardData).toolType == "ride")
                    {
                        if(((PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData).traits.tags.Contains("rideable"))
                        {
                            GameManager.Instance.ShowConfirmationButtons("select cards for payment");
                            HandScript.Instance.state = "buildingPay";
                            HandScript.Instance.Raise();
                            HandScript.Instance.updateSelection = VerifyButtonsForPalCardPurchase;
                            ConfirmationButtons.Instance.Confirmed += PayForPalCard;
                            ConfirmationButtons.Instance.Denied += Disengage;
                            ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;

                            VerifyButtonsForPalCardPurchase();
                        }
                    }
                }
            }
            else
            {
                OpenContextMenu(gameObject);
            }
        }
        else if(HandScript.Instance.state == "lookingForSphere")
        {
            HandScript.Instance.Select(gameObject);
        }

    }

    public void CheckForCard()
    {
        transform.parent.SendMessage("CheckForCard");
    }

    public override void GiveCardEventActions()
    {
        base.GiveCardEventActions();
        StartPlayerTurn += PrepareMainPhase;

        if(GameManager.Instance.phase == "PlayerTurn")
            PrepareMainPhase();
    }

    public void PlaceOnCorrectSpot(GameObject card)
    {
        gameObject.transform.parent.SendMessage("PlaceOnCorrectSpot", card);
    }
    
    public override bool CanBeBooted()
    {
        return true;
    }

    #region saddleStuffs

    private void StackPalCard(GameObject card)
    {
        heldCard = card;
        heldCard.transform.SetParent(transform);
        heldCard.transform.position = transform.position;
        heldCard.GetComponent<PalCardScript>().opponentMirror = opponentMirror;

        if(waitingSpace.GetComponent<WaitingSpace>().readyCards.Contains(card))
        {
            // opponentMirror.RPC("GetCardFromWaitingSpace", RpcTarget.Others, waitingSpace.GetComponent<WaitingSpace>().readyCards.IndexOf(card));
            waitingSpace.GetComponent<WaitingSpace>().readyCards.RemoveAt(waitingSpace.GetComponent<WaitingSpace>().readyCards.IndexOf(card));
        }

        heldCard.GetComponent<PalCardScript>().PlaceOnSpot();
        HideHealthCounter();
    }

    private void PayForPalCard()
    {
        HandScript.Instance.updateSelection -= VerifyButtonsForPalCardPurchase;
        ConfirmationButtons.Instance.Confirmed -= PayForPalCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        ConfirmationButtons.Instance.Denied -= HandScript.Instance.ClearSelection;
        GameManager.Instance.HideConfirmationButtons();

        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;
        opponentMirror.RPC("StackCard", RpcTarget.Others, data.originalData.cardID);

        if(data.size <= 1)
        {
            heldCard = Instantiate(palCardPrefab, transform.position, transform.rotation);
            heldCard.SendMessage("SetUpCard", data);
            StackPalCard(heldCard);
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

    private void Disengage()
    {
        HandScript.Instance.updateSelection -= VerifyButtonsForPalCardPurchase;
        ConfirmationButtons.Instance.Confirmed -= PayForPalCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        ConfirmationButtons.Instance.Denied -= HandScript.Instance.ClearSelection;
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.state = "default";
    }

    private void VerifyButtonsForPalCardPurchase()
    {
        ConfirmationButtons.Instance.AllowConfirmation(ResourceProcesses.PalPaymentIsCorrect());
    }


    public void LoseHeldCard()
    {
        heldCard = null;
    }

    public void BreakSphere()
    {
        Die();
        // opponentMirror.RPC("BreakPalSphere", RpcTarget.Others);
    }


#endregion
}
