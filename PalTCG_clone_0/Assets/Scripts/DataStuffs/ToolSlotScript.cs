using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using Resources;
using DefaultUnitData;
public class ToolSlotScript : CardHolderScript
{
    protected Image image;
    public string slotType;
    public static List<ToolSlotScript> allToolSlots = new List<ToolSlotScript>();

    public void Awake()
    {
        image = gameObject.GetComponent<Image>();
        allToolSlots.Add(gameObject.GetComponent<ToolSlotScript>());
    }

    public void CheckForCard()
    {
        if(GameManager.Instance.phase == "PlayerTurn")
        {
            if(HandScript.Instance.selected != null && HandScript.Instance.state == "default")
            {
                if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is ToolCardData)
                {
                    GameManager.Instance.ShowConfirmationButtons();
                    HandScript.Instance.state = "awaitingDecision";
                    HandScript.Instance.updateSelection += VerifyButtons;
                    ConfirmationButtons.Instance.Confirmed += PayForCard;
                    ConfirmationButtons.Instance.Denied += DisengagePurchase;
                }

                VerifyButtons();
            }
            else if(HandScript.Instance.state == "lookingForSlot")
            {
                HandScript.Instance.Select(gameObject);
            }
        }
        else if(HandScript.Instance.selected != null)
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                HandScript.Instance.selected.SendMessage("Deselect");
                HandScript.Instance.selected = null;
            }
        }
    }

    public void PlaceOnCorrectSpot(GameObject card)
    {
        var slot = FindMatchingSlot(((ToolCardData)card.GetComponent<ToolCardScript>().cardData).toolType);
        slot?.PlaceCard(card);
    }

    public static void ForceEquipCardToCorrectSlot(GameObject card, string toolType)
    {
        var slot = FindMatchingSlot(toolType);

        slot?.ForcePlaceCard(card);
    }

    void PlaceCard(GameObject card)
    {
        if(heldCard == null)
        {
            heldCard = card;
            heldCard.transform.SetParent(gameObject.transform); //Need to make it search for the matching one
            heldCard.transform.position = transform.position;
            heldCard.GetComponent<ToolCardScript>().opponentMirror = opponentMirror;

            if(waitingSpace.GetComponent<WaitingSpace>().readyCards.Contains(card))
            {
                opponentMirror.RPC("GetCardFromWaitingSpace", RpcTarget.Others, waitingSpace.GetComponent<WaitingSpace>().readyCards.IndexOf(card));
                waitingSpace.GetComponent<WaitingSpace>().readyCards.RemoveAt(waitingSpace.GetComponent<WaitingSpace>().readyCards.IndexOf(card));
            }

            heldCard.GetComponent<ToolCardScript>().PlaceOnSpot();
            
            heldCard.GetComponent<ToolCardScript>().waitingSpace = waitingSpace;
        }
        else
        {
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.state = "awaitingDecision";
            ConfirmationButtons.Instance.Confirmed += (() => heldCard.SendMessage("SendToPalBox"));
            ConfirmationButtons.Instance.Confirmed += (() => PlaceCard(card));
            ConfirmationButtons.Instance.Confirmed += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.Denied += (() => waitingSpace.SendMessage("AddToReadySpot", card));
            ConfirmationButtons.Instance.Denied += AllowConfirmations.ClearButtonEffects;
            ConfirmationButtons.Instance.AllowConfirmation(true);
        }
        
    }

    public void ForcePlaceCard(GameObject card)
    {
        if(heldCard != null)
            heldCard.SendMessage("SendToPalBox");
        
        heldCard = card;
        heldCard.transform.SetParent(gameObject.transform); //Need to make it search for the matching one
        heldCard.transform.position = transform.position;
        heldCard.GetComponent<UnitCardScript>().opponentMirror = opponentMirror;

        heldCard.GetComponent<UnitCardScript>().PlaceOnSpot();
        
        if(heldCard.GetComponent<ToolSlotScript>() != null)
            heldCard.GetComponent<ToolCardScript>().waitingSpace = waitingSpace;

        opponentMirror.RPC("RecieveMovingCard", RpcTarget.Others);
    }

    private void PayForCard()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PayForCard;
        ConfirmationButtons.Instance.Denied -= DisengagePurchase;
        ConfirmationButtons.Instance.Denied -= HandScript.Instance.ClearSelection;
        GameManager.Instance.HideConfirmationButtons();

        var data = (ToolCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;


        if(data.size <= 1)
        {
            var newCard = Instantiate(cardPrefab, transform.position, transform.rotation);
            var slot = FindMatchingSlot(data.toolType);

            slot?.opponentMirror.RPC("CreateCard", RpcTarget.Others, data.originalData.cardID);
            newCard.SendMessage("SetUpCard", data);
            slot?.PlaceCard(newCard);
        }
        else
        {
            opponentMirror.RPC("CreateCard", RpcTarget.Others, data.originalData.cardID);
            waitingSpace.SendMessage("AddToWaitlist", data);
        }

        HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
        Destroy(HandScript.Instance.selected);
        HandScript.Instance.selected = null;

        HandScript.Instance.GatheredItems -= data.cost;

        HandScript.Instance.state = "default";
    }

    public void LoseHeldCard()
    {
        heldCard = null;
    }

    private static ToolSlotScript FindMatchingSlot(string slotName)
    {
        ToolSlotScript matchingSlot = null;
        foreach(ToolSlotScript nextSlot in allToolSlots)
        {
            if(nextSlot.slotType == slotName)
            {
                matchingSlot = nextSlot;
                break;
            }
        }

        return matchingSlot;
    }

    void DisengagePurchase()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PayForCard;
        ConfirmationButtons.Instance.Denied -= DisengagePurchase;
        ConfirmationButtons.Instance.Denied -= HandScript.Instance.ClearSelection;
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.state = "default";
    }

    void VerifyButtons()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.GatheredItems >= ((ToolCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData).cost);
    }

    public void BreakSphere()
    {
        Debug.Log("This doesn't break");
    }
}
