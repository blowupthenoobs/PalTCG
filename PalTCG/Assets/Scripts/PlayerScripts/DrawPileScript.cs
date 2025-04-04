using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultUnitData;

public class DrawPileScript : MonoBehaviour
{
    public List<CardData> currentDeck = new List<CardData>();

    public void SetDeckCards(string deck)
    {
        string[] cards = deck.Split(",");

        List<CardData> cardsToAdd = new List<CardData>();

        for(int i = 0; i < cards.Length; i++)
        {
            cardsToAdd.Add(Pals.ConvertToCardData(cards[i]));
        }

        while(cardsToAdd.Count > 0)
        {
            var index = Random.Range(0, cardsToAdd.Count);
            currentDeck.Add(cardsToAdd[index]);
            cardsToAdd.RemoveAt(index);
        }
    }

    public void LookAtRemainingDeck()
    {
        GameManager.Instance.CardPileBox.GetComponent<CardListPopupScript>().LookAtRemainingDeck(currentDeck);
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
