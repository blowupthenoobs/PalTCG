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

    List<GameObject> readyCards = new List<GameObject>();
    List<GameObject> TurnsTillReady1 = new List<GameObject>();
    List<GameObject> TurnsTillReady2 = new List<GameObject>();
    List<GameObject> TurnsTillReady3 = new List<GameObject>();

    [SerializeField] bool isPlayerSide;


    [SerializeField] GameObject cardPrefab;

    void Start() //Do this first to garuentee that GameManager has a value as start happens after awake
    {
        if(isPlayerSide)
            GameManager.Instance.StartPlayerTurn += MoveWaitlist;
        else
            GameManager.Instance.StartEnemyTurn += MoveWaitlist;
    }

    [PunRPC]
    public void CreateCardForWaitlist(string palType)
    {
        var data = (PalCardData)Pals.ConvertToCardData(palType);

        AddToWaitlist(data);

        //RemoveCards from opponent hand?
    }

    public void AddToWaitlist(PalCardData data)
    {
        int size = data.size;

        var heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);

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

    public void CheckForCard()
    {
        if(HandScript.Instance.selected != null && HandScript.Instance.state == "default")
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                GameManager.Instance.ShowConfirmationButtons();
                HandScript.Instance.state = "buildingPay";
                HandScript.Instance.Raise();
                HandScript.Instance.updateSelection += VerifyButtons;
                ConfirmationButtons.Instance.Confirmed += PlaceCard;
                ConfirmationButtons.Instance.Denied += Disengage;

                VerifyButtons();
            }
        }
    }

    void PlaceCard()
    {
        HandScript.Instance.updateSelection -= VerifyButtons;
        ConfirmationButtons.Instance.Confirmed -= PlaceCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
        GameManager.Instance.HideConfirmationButtons();

        var data = (PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData;

        AddToWaitlist(data);
        opponentMirror.RPC("CreateCardForWaitlist", RpcTarget.Others, data.originalData.cardID);

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
        ConfirmationButtons.Instance.Confirmed -= PlaceCard;
        ConfirmationButtons.Instance.Denied -= Disengage;
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
