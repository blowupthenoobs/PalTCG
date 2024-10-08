using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileScript : MonoBehaviour
{
    public new List<string> currentDeck = new List<string>();

    void Start()
    {
        SetDeckCards(HandScript.Instance.Preferences.tempDeck);
    }

    public void SetDeckCards(string deck)
    {
        string[] cards = deck.Split(",");

        List<string> cardsToAdd = new List<string>();

        for(int i = 0; i < cards.Length; i++)
        {
            cardsToAdd.Add(cards[i]);
        }

        while(cardsToAdd.Count > 0)
        {
            var index = Random.Range(0, cardsToAdd.Count);
            currentDeck.Add(cardsToAdd[index]);
            cardsToAdd.RemoveAt(index);
        }
    }

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
