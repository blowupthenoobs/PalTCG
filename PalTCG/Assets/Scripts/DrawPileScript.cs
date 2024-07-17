using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileScript : MonoBehaviour
{
    public new List<CardData> currentDeck = new List<CardData>();

    public void Draw()
    {
        if(currentDeck.Count > 0)
        {
            HandScript.Instance.Draw(currentDeck[0]);
            currentDeck.RemoveAt(0);
        }
        else
            Debug.Log("out of cards");
    }
}
