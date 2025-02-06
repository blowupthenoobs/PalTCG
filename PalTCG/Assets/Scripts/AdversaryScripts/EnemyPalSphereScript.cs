using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using DefaultUnitData;

public class EnemyPalSphereScript : MonoBehaviour
{
    public PhotonView opponentMirror;
    [SerializeField] GameObject waitingSpace;
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

#region palsphereFunctions
    public void SelectAsTarget()
    {
        if(heldCard != null)
        {
            if(HandScript.Instance.state == "targeting")
            {
                GameManager.Instance.ShowConfirmationButtons();
                HandScript.Instance.state = "raiding";
                HandScript.Instance.selection.Add(HandScript.Instance.selected);
                HandScript.Instance.selected = heldCard;
                heldCard.SendMessage("Select");
                HandScript.Instance.updateSelection += VerifyAttack;
                ConfirmationButtons.Instance.Confirmed += StartRaid;
                ConfirmationButtons.Instance.Confirmed += () => HandScript.Instance.StartCoroutine(HandScript.Instance.Attack());
                ConfirmationButtons.Instance.Denied += DisengageAttacks;
                ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
            }
            else if(HandScript.Instance.state == "settingAilment")
            {
                HandScript.Instance.Select(heldCard);
            }
        }
        
    }

    void DisengageAttacks()
    {
        
        HandScript.Instance.updateSelection -= VerifyAttack;
        ConfirmationButtons.Instance.Confirmed = null;
        ConfirmationButtons.Instance.Denied = null;
        GameManager.Instance.HideConfirmationButtons();
        heldCard.SendMessage("Deselect");

        HandScript.Instance.state = "choosingAttack";
    }

    void StartRaid()
    {
        HandScript.Instance.updateSelection -= VerifyAttack;
        ConfirmationButtons.Instance.Confirmed = null;
        ConfirmationButtons.Instance.Denied = null;
        GameManager.Instance.HideConfirmationButtons();
        heldCard.SendMessage("Deselect");

        HandScript.Instance.state = "choosingAttack";
    }

    [PunRPC]
    void GetCardFromWaitingSpace(int cardIndex)
    {
        PlaceCard(waitingSpace.GetComponent<WaitingSpace>().readyCards[cardIndex]);
        waitingSpace.GetComponent<WaitingSpace>().readyCards.RemoveAt(cardIndex);
    }

    [PunRPC]
    void CreateCard(string palType)
    {
        var data = (PalCardData)Pals.ConvertToCardData(palType);

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
    }

    void PlaceCard(GameObject card)
    {
        // heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard = card;
        heldCard.transform.SetParent(transform);
        heldCard.transform.position = transform.position;
    }

    public void VerifyAttack()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count > 0);
    }
#endregion

#region cardDelegation
    [PunRPC]
    public void UpdateHealth(int newHealth)
    {
        Debug.Log("Updating health");
        heldCard.GetComponent<EnemyPalCardScript>().cardData.currentHp = newHealth;
        heldCard.GetComponent<EnemyPalCardScript>().health.text = newHealth.ToString();
    }

    [PunRPC]
    public void NormalRest()
    {
        heldCard.SendMessage("Rest");
    }


    public void SendRestEffect()
    {
        opponentMirror.RPC("Rest", RpcTarget.Others);
    }
#endregion
}
