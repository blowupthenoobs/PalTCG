using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphereScript : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

    public void CheckForCard()
    {
        if(HandScript.Instance.selected != null && heldCard == null)
        {
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
            {
                PlaceCard((PalCardData)HandScript.Instance.selected.GetComponent<CardScript>().cardData);
            }
        }
    }

    void PlaceCard(PalCardData data)
    {
        HandScript.Instance.buildingPay = true;

        if(PayCost(data))
        {
            heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
            heldCard.transform.SetParent(transform);

            heldCard.GetComponent<PalCardScript>().cardData = data;

            HandScript.Instance.Hand.RemoveAt(HandScript.Instance.Hand.IndexOf(HandScript.Instance.selected));
            Destroy(HandScript.Instance.selected);
        }
        
    }

    bool PayCost(PalCardData data)
    {
        Debug.Log("called");
        var cost = data.cost;

        // while(HandScript.Instance.payment != cost)
        // {
        //     Debug.Log("waiting");
        // }

        return true;
    }
}
