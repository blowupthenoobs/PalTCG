using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using DefaultUnitData;

public class EnemyPalSphereScript : MonoBehaviour
{
    public PhotonView opponentMirror;
    private Image image;
    [SerializeField] GameObject waitingSpace;
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;
    
    [SerializeField] Color normalColor;
    [SerializeField] Color brokenColor;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

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

        HandScript.Instance.state = "runningAttacks";
    }

    [PunRPC]
    void GetCardFromWaitingSpace(int cardIndex)
    {
        PlaceCard(waitingSpace.GetComponent<WaitingSpace>().readyCards[cardIndex]);
        waitingSpace.GetComponent<WaitingSpace>().readyCards.RemoveAt(cardIndex);
    }

    [PunRPC]
    void CreateCard(string cardString)
    {
        var data = Pals.ConvertToCardData(cardString);

        if(data is PalCardData palData)
        {
            if(palData.size <= 1)
            {
                heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
                PlaceCard(heldCard);

                heldCard.SendMessage("SetUpCard", palData);
            }
            else
            {
                waitingSpace.SendMessage("AddToWaitlist", palData);
            }
        }
        else if(data is ToolCardData toolData)
        {
            if(toolData.size <= 1)
            {
                heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
                PlaceCard(heldCard);

                heldCard.SendMessage("SetUpCard", toolData);
            }
            else
            {
                waitingSpace.SendMessage("AddToWaitlist", toolData);
            }
        }
        
    }

    void PlaceCard(GameObject card)
    {
        // heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard = card;
        heldCard.transform.SetParent(transform);
        heldCard.transform.position = transform.position;
        heldCard.SendMessage("SetMirror", opponentMirror);
    }

    public void VerifyAttack()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count > 0);
    }

    [PunRPC]
    public void BreakPalSphere()
    {
        image.color = brokenColor;
    }
#endregion

#region cardDelegation
    [PunRPC]
    public void StackCard(string cardString)
    {
        var data = Pals.ConvertToCardData(cardString);

        if(data is PalCardData palData)
        {
            if(palData.size <= 1)
            {
                var stackedCard = Instantiate(cardPrefab, transform.position, transform.rotation);
                heldCard.SendMessage("StackCard", stackedCard);

                stackedCard.SendMessage("SetUpCard", palData);
            }
            else
            {
                waitingSpace.SendMessage("AddToWaitlist", palData);
            }
        }
        else if(data is ToolCardData toolData)
        {
            if(toolData.size <= 1)
            {
                heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
                PlaceCard(heldCard);

                heldCard.SendMessage("SetUpCard", toolData);
            }
            else
            {
                waitingSpace.SendMessage("AddToWaitlist", toolData);
            }
        }
    }

    [PunRPC]
    public void UpdateHealth(int newHealth)
    {
        heldCard.SendMessage("UpdateHealth", newHealth);
    }

    [PunRPC]
    public void NormalRest()
    {
        heldCard.SendMessage("Rest");
    }

    [PunRPC]
    public void Wake()
    {
        heldCard.SendMessage("Wake");
    }

    [PunRPC]
    public void Block()
    {
        HandScript.Instance.blocker = heldCard;
    }

    [PunRPC]
    public void HeldUnitDeath()
    {
        heldCard.SendMessage("Die");
    }
#endregion
}
