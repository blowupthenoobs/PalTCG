using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultUnitData;

public class CardListPopupScript : MonoBehaviour
{
    [SerializeField] GameObject IconPrefab;
    List<CardData> defaultDeckTypes = new List<CardData>();
    List<GameObject> tempPileItems = new List<GameObject>();

    public void GetDefaultData(string deck)
    {
        string[] cards = deck.Split(",");


        for(int i = 0; i < cards.Length; i++)
        {
            var data = Pals.ConvertToCardData(cards[i]);

            if(!defaultDeckTypes.Contains(data))
                defaultDeckTypes.Add(data);

        }
    }

    public void CreateCardIcons(List<CardData> pileList)
    {
        List<CardData> cardTypes = new List<CardData>();
        List<int> cardCount = new List<int>();

        for(int i = 0; i < pileList.Count; i++)
        {
            if(!cardTypes.Contains(pileList[i]))
            {
                cardTypes.Add(pileList[i]);
                cardCount.Add(1);
            }
            else
                cardCount[cardTypes.IndexOf(pileList[i])]++;

        }

        while(cardTypes.Count > 0)
        {
            tempPileItems.Add(Instantiate(IconPrefab, transform.position, transform.rotation));
            tempPileItems[defaultDeckTypes.Count - 1].GetComponent<PileViewCardIconScript>().SetUpIcon(cardTypes[0], cardCount[0]);
            tempPileItems[defaultDeckTypes.Count - 1].transform.SetParent(transform);

            cardTypes.RemoveAt(0);
            cardCount.RemoveAt(0);
        }
    }

    public void LookInDeckPile(List<CardData> pileList)
    {
        List<CardData> cardTypes = new List<CardData>(defaultDeckTypes);
        List<int> cardCount = new List<int>();

        for(int i = 0; i < pileList.Count; i++)
        {
            if(!cardTypes.Contains(pileList[i]))
            {
                cardTypes.Add(pileList[i]);
                cardCount.Add(1);
            }
            else
                cardCount[cardTypes.IndexOf(pileList[i])]++;

        }

        while(cardTypes.Count > 0)
        {
            tempPileItems.Add(Instantiate(IconPrefab, transform.position, transform.rotation));
            tempPileItems[defaultDeckTypes.Count - 1].GetComponent<PileViewCardIconScript>().SetUpIcon(cardTypes[0], cardCount[0]);
            tempPileItems[defaultDeckTypes.Count - 1].transform.SetParent(transform);

            cardTypes.RemoveAt(0);
            cardCount.RemoveAt(0);
        }
    }
}
