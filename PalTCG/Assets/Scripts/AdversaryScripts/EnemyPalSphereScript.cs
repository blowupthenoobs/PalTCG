using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPalSphereScript : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

    void Start()
    {
        PlaceCard();
    }

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

    void PlaceCard()
    {
        heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard.transform.SetParent(transform);
    }

    public void VerifyAttack()
    {
        ConfirmationButtons.Instance.AllowConfirmation(HandScript.Instance.selection.Count > 0);
    }
}
