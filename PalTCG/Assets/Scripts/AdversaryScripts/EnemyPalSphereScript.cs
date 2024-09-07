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
        if(HandScript.Instance.state == "targeting" && heldCard != null)
        {
            GameManager.Instance.ShowConfirmationButtons();
            HandScript.Instance.state = "raiding";
            Debug.Log("now running");
            // HandScript.Instance.updateSelection += VerifyButtons;
            // ConfirmationButtons.Instance.Confirmed += PayForCard;
            // ConfirmationButtons.Instance.Denied += Disengage;
            // ConfirmationButtons.Instance.Denied += HandScript.Instance.ClearSelection;
        }
    }

    void PlaceCard()
    {
        heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard.transform.SetParent(transform);
    }
}
