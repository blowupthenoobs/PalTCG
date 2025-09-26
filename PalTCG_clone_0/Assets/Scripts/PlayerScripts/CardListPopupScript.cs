using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DefaultUnitData;

public class CardListPopupScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject IconPrefab;
    public List<CardData> defaultDeckTypes = new List<CardData>();
    List<GameObject> tempPileItems = new List<GameObject>();
    private bool cancelDissapear;
    public static bool lookingAtDiscard;
    private bool isHovered;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isHovered) //
            HidePopUp();
    }

    public void GetDefaultData(string deck)
    {
        string[] cards = deck.Split(",");


        for (int i = 0; i < cards.Length; i++)
        {
            var data = Pals.ConvertToCardData(cards[i]);

            if (!defaultDeckTypes.Contains(data))
                defaultDeckTypes.Add(data);

        }
    }

    public void CreateCardIcons(List<CardData> pileList, bool isDiscard = false)
    {
        lookingAtDiscard = isDiscard;
        gameObject.SetActive(true);

        List<CardData> cardTypes = new List<CardData>();
        List<int> cardCount = new List<int>();

        for (int i = 0; i < pileList.Count; i++)
        {
            if (!cardTypes.Contains(pileList[i]))
            {
                cardTypes.Add(pileList[i]);
                cardCount.Add(1);
            }
            else
                cardCount[cardTypes.IndexOf(pileList[i])]++;

        }

        while (cardTypes.Count > 0)
        {
            tempPileItems.Add(Instantiate(IconPrefab, transform.position, transform.rotation));
            tempPileItems[tempPileItems.Count - 1].GetComponent<PileViewCardIconScript>().SetUpIcon(cardTypes[0], cardCount[0]);
            tempPileItems[tempPileItems.Count - 1].transform.SetParent(transform);

            cardTypes.RemoveAt(0);
            cardCount.RemoveAt(0);
        }

    }

    public void LookAtRemainingDeck(List<CardData> pileList)
    {
        gameObject.SetActive(true);

        List<CardData> cardTypes = new List<CardData>(defaultDeckTypes);
        List<int> cardCount = new List<int>();

        for (int i = 0; i < cardTypes.Count; i++)
        {
            cardCount.Add(0);
        }

        for (int i = 0; i < defaultDeckTypes.Count; i++)
        {
            if (!pileList.Contains(defaultDeckTypes[i]))
            {
                cardCount.RemoveAt(cardTypes.IndexOf(defaultDeckTypes[i]));
                cardTypes.RemoveAt(cardTypes.IndexOf(defaultDeckTypes[i]));
            }
        }

        foreach (CardData card in pileList)
        {
            cardCount[cardTypes.IndexOf(card)]++;
        }

        while (cardTypes.Count > 0)
        {
            tempPileItems.Add(Instantiate(IconPrefab, transform.position, transform.rotation));
            tempPileItems[tempPileItems.Count - 1].GetComponent<PileViewCardIconScript>().SetUpIcon(cardTypes[0], cardCount[0]);
            tempPileItems[tempPileItems.Count - 1].transform.SetParent(transform);

            cardTypes.RemoveAt(0);
            cardCount.RemoveAt(0);
        }
    }

    public void Click()
    {
        cancelDissapear = true;
    }

    private IEnumerator ClickOffMenu()
    {
        yield return new WaitForSeconds(.1f);

        if (cancelDissapear)
            cancelDissapear = false;
        else
            HidePopUp();
    }

    private void HidePopUp()
    {
        while(tempPileItems.Count > 0)
        {
            Destroy(tempPileItems[0]);
            tempPileItems.RemoveAt(0);
        }

        gameObject.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void DeselectItemOfDataType(CardData cardType)
    {
        foreach(GameObject card in tempPileItems)
        {
            if(card.GetComponent<PileViewCardIconScript>().cardData == cardType)
            {
                card.SendMessage("Deselect");
                break;
            }
        }
    }
    
}
