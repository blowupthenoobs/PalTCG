using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultUnitData;

public class DrawPileScript : MonoBehaviour
{
    public new List<CardData> currentDeck = new List<CardData>();

    void Start()
    {
        SetDeckCards(HandScript.Instance.Preferences.tempDeck);
    }

    public void SetDeckCards(string deck)
    {
        string[] cards = deck.Split(",");

        List<CardData> cardsToAdd = new List<CardData>();

        for(int i = 0; i < cards.Length; i++)
        {
            cardsToAdd.Add(ConvertToCardData(cards[i]));
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

    private CardData ConvertToCardData(string datatype)
    {
        string[] dataParts = datatype.Split("/");

        CardData data = null;

        switch(dataParts[0])
        {
            case "p":
                data = (PalCardData)ScriptableObject.CreateInstance(typeof(PalCardData));
                ((PalCardData)data).DecomposeData(new Pals().FindPalData(dataParts[1], int.Parse(dataParts[2])));
            break;
            case "t":
                Debug.Log("look for items");
            break;
            case "h":
                Debug.Log("look for player/hero");
            break;
        }

        return data;
    }
}
