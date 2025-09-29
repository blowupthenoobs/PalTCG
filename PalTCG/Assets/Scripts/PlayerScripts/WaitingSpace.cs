using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using Resources;
using DefaultUnitData;

public class WaitingSpace : MonoBehaviour
{
    [SerializeField] PhotonView opponentMirror;
    [SerializeField] GameObject waiting3;
    [SerializeField] GameObject waiting2;
    [SerializeField] GameObject waiting1;
    [SerializeField] GameObject readyspot;

    public List<GameObject> readyCards = new List<GameObject>();
    List<GameObject> TurnsTillReady1 = new List<GameObject>();
    List<GameObject> TurnsTillReady2 = new List<GameObject>();
    List<GameObject> TurnsTillReady3 = new List<GameObject>();

    [SerializeField] bool isPlayerSide;


    [SerializeField] GameObject PalCardPrefab;
    [SerializeField] GameObject ToolCardPrefab;

    void Start() //Do this first to garuentee that GameManager has a value as start happens after awake
    {
        if(isPlayerSide)
            GameManager.Instance.StartPlayerTurn += MoveWaitlist;
        else
            GameManager.Instance.StartEnemyTurn += MoveWaitlist;
    }

    [PunRPC]
    public void CreateCardForWaitlist(string cardType)
    {
        var data = Pals.ConvertToCardData(cardType);

        if(data is PalCardData)
            AddToWaitlist((PalCardData)data);
        if(data is ToolCardData)
            AddToWaitlist((ToolCardData)data);
            
        //RemoveCards from opponent hand?
    }

    public void AddToWaitlist(PalCardData data)
    {
        int size = data.size;

        var heldCard = Instantiate(PalCardPrefab, transform.position, transform.rotation);

        if(size <= 1)
        {
            heldCard.transform.position = readyspot.transform.position;
            heldCard.transform.SetParent(readyspot.transform);
            readyCards.Add(heldCard);

            if(isPlayerSide)
                heldCard.SendMessage("ReadyToBePlaced");
        }
        else if(size == 2)
        {
            heldCard.transform.position = waiting1.transform.position;
            heldCard.transform.SetParent(waiting1.transform);
            TurnsTillReady1.Add(heldCard);
        }
        else if(size == 3)
        {
            heldCard.transform.position = waiting2.transform.position;
            heldCard.transform.SetParent(waiting2.transform);
            TurnsTillReady2.Add(heldCard);
        }
        else if(size == 4)
        {
            heldCard.transform.position = waiting3.transform.position;
            heldCard.transform.SetParent(waiting3.transform);
            TurnsTillReady3.Add(heldCard);
        }

        heldCard.SendMessage("SetUpCard", data);
    }

    public void AddToWaitlist(ToolCardData data)
    {
        int size = data.size;

        var heldCard = Instantiate(ToolCardPrefab, transform.position, transform.rotation);

        if(size <= 1)
        {
            heldCard.transform.position = readyspot.transform.position;
            heldCard.transform.SetParent(readyspot.transform);
            readyCards.Add(heldCard);

            if(isPlayerSide)
                heldCard.SendMessage("ReadyToBePlaced");
        }
        else if(size == 2)
        {
            heldCard.transform.position = waiting1.transform.position;
            heldCard.transform.SetParent(waiting1.transform);
            TurnsTillReady1.Add(heldCard);
        }
        else if(size == 3)
        {
            heldCard.transform.position = waiting2.transform.position;
            heldCard.transform.SetParent(waiting2.transform);
            TurnsTillReady2.Add(heldCard);
        }
        else if(size == 4)
        {
            heldCard.transform.position = waiting3.transform.position;
            heldCard.transform.SetParent(waiting3.transform);
            TurnsTillReady3.Add(heldCard);
        }

        heldCard.SendMessage("SetUpCard", data);
    }

    void MoveWaitlist()
    {
        while(TurnsTillReady1.Count > 0)
        {
            TurnsTillReady1[0].transform.SetParent(readyspot.transform);
            
            if(isPlayerSide)
                TurnsTillReady1[0].SendMessage("ReadyToBePlaced");
            
            readyCards.Add(TurnsTillReady1[0]);
            TurnsTillReady1.RemoveAt(0);
        }

        while(TurnsTillReady2.Count > 0)
        {
            TurnsTillReady2[0].transform.SetParent(waiting1.transform);
            TurnsTillReady1.Add(TurnsTillReady2[0]);
            TurnsTillReady2.RemoveAt(0);
        }

        while(TurnsTillReady3.Count > 0)
        {
            TurnsTillReady3[0].transform.SetParent(waiting2.transform);
            TurnsTillReady2.Add(TurnsTillReady3[0]);
            TurnsTillReady3.RemoveAt(0);
        }
    }

    public void AddToReadySpot(GameObject newCard)
    {
        newCard.transform.position = readyspot.transform.position;
        newCard.transform.SetParent(readyspot.transform);

        if(!readyCards.Contains(newCard))
        {
            readyCards.Add(newCard);

            if(isPlayerSide)
                newCard.SendMessage("ReadyToBePlaced");
        }

        HandScript.Instance.state = "default";
    }

    public void CheckForCard()
    {
        if(HandScript.Instance.selected != null && HandScript.Instance.state == "default")
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is PalCardData)
                {
                    GameManager.Instance.ShowConfirmationButtons("select cards for payment");
                    HandScript.Instance.state = "buildingPay";
                    HandScript.Instance.Raise();
                    HandScript.Instance.updateSelection = VerifyButtonsForPalCard;
                    ConfirmationButtons.Instance.Confirmed += PlaceCard;
                    ConfirmationButtons.Instance.Denied += Disengage;

                    VerifyButtonsForPalCard();
                }
                else if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is ToolCardData)
                {
                    GameManager.Instance.ShowConfirmationButtons("Pay materials for tool?");
                    HandScript.Instance.state = "awaitingDecision";
                    HandScript.Instance.Raise();
                    HandScript.Instance.updateSelection = VerifyButtonsForToolCard;
                    ConfirmationButtons.Instance.Confirmed += PlaceCard;
                    ConfirmationButtons.Instance.Denied += Disengage;

                    VerifyButtonsForToolCard();
                }
                
            }
        }
    }

    void PlaceCard()
    {
        AllowConfirmations.ClearButtonEffects();
        GameManager.Instance.HideConfirmationButtons();

        if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is PalCardData palData)
        {
            AddToWaitlist(palData);
            opponentMirror.RPC("CreateCardForWaitlist", RpcTarget.Others, palData.originalData.cardID);

            HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
            Destroy(HandScript.Instance.selected);

            while(HandScript.Instance.selection.Count > 0)
            {
                var cardToDiscard = HandScript.Instance.selection[0];
                HandScript.Instance.selection.RemoveAt(0);
                HandScript.Instance.Discard(cardToDiscard);
            }
        }
        if(HandScript.Instance.selected.GetComponent<CardScript>().cardData is ToolCardData toolData)
        {
            AddToWaitlist(toolData);
            opponentMirror.RPC("CreateCardForWaitlist", RpcTarget.Others, toolData.originalData.cardID);

            HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
            Destroy(HandScript.Instance.selected);

            HandScript.Instance.GatheredItems -= toolData.cost;
        }
        

        HandScript.Instance.state = "default";
    }

    public void PlaceCard(CardData data)
    {
        AllowConfirmations.ClearButtonEffects();
        GameManager.Instance.HideConfirmationButtons();

        if(data is PalCardData palData)
        {
            AddToWaitlist(palData);
            opponentMirror.RPC("CreateCardForWaitlist", RpcTarget.Others, palData.originalData.cardID);

            while(HandScript.Instance.selection.Count > 0)
            {
                var cardToDiscard = HandScript.Instance.selection[0];
                HandScript.Instance.selection.RemoveAt(0);
                HandScript.Instance.Discard(cardToDiscard);
            }
        }
        if(data is ToolCardData toolData)
        {
            AddToWaitlist(toolData);
            opponentMirror.RPC("CreateCardForWaitlist", RpcTarget.Others, toolData.originalData.cardID);

            HandScript.Instance.GatheredItems -= toolData.cost;
        }
        

        HandScript.Instance.state = "default";
    }

    void Disengage()
    {
        AllowConfirmations.ClearButtonEffects();
        GameManager.Instance.HideConfirmationButtons();

        HandScript.Instance.selected.SendMessage("Deselect");
        HandScript.Instance.selected = null;

        HandScript.Instance.state = "default";
    }

    void VerifyButtonsForPalCard()
    {
        ConfirmationButtons.Instance.AllowConfirmation(ResourceProcesses.PalPaymentIsCorrect());
    }
    
    void VerifyButtonsForToolCard()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.GatheredItems >= ((ToolCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData).cost);
    }
}
