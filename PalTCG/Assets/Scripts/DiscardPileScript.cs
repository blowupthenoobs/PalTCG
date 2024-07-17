using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPileScript : MonoBehaviour
{
    public static DiscardPileScript Instance;
    public DrawPileScript drawPile;
    public new List<CardData> discardPile = new List<CardData>();

    void Awake()
    {
        if(Instance == null)
            Instance=this;
        else
            Destroy(gameObject);
    }
    
    public void Restock()
    {
        // while(discardPile.Count > 0)
        // {
        //     int index = Random.Range(0, discardPile.Count);

        //     drawPile.drawPile.Add(discardPile[index]);
        //     drawPile.cardData.Add(cardData[index]);

        //     discardPile.RemoveAt(index);
        //     cardData.RemoveAt(index);
        // }
    }
}
