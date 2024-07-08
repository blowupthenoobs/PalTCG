using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphereScript : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    public GameObject heldCard;

    public void CheckForCard()
    {
        if(HandScript.Instance.selected != null)
        {
            Debug.Log("passed");
            if(HandScript.Instance.selected.GetComponent<CardScript>() != null)
                PlaceCard(HandScript.Instance.selected.GetComponent<CardScript>().cardData);
        }
    }

    void PlaceCard(CardData data)
    {
        heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard.transform.SetParent(transform);

        heldCard.GetComponent<PalCardScript>().cardData = data;
    }
}
