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

    void PlaceCard()
    {
        heldCard = Instantiate(cardPrefab, transform.position, transform.rotation);
        heldCard.transform.SetParent(transform);
    }
}
