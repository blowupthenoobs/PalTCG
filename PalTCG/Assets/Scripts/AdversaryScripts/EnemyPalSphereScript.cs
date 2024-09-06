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

    void Update()
    {
        
    }

    public void SelectAsTarget()
    {
        Debug.Log(HandScript.Instance.state);
        if(GameManager.Instance.phase == "targeting" && heldCard != null)
        {
            if(HandScript.Instance.selected != null && heldCard == null && HandScript.Instance.state == "default")
            {
                if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
                {
                    GameManager.Instance.ShowConfirmationButtons();
                    HandScript.Instance.state = "raiding";
                    Debug.Log(HandScript.Instance.state);
                    // HandScript.Instance.updateSelection += VerifyButtons;
                    // ConfirmationButtons.Instance.Confirmed += PayForCard;
                    // ConfirmationButtons.Instance.Denied += Disengage;
                    // ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
                }
            }
            else if(HandScript.Instance.state == "lookingForSphere")
            {
                HandScript.Instance.Select(gameObject);
            }
        }
    }

    void PlaceCard()
    {
        heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard.transform.SetParent(transform);
    }
}
