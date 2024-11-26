using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using DefaultUnitData;

public class DeckBuildingManagerScript : MonoBehaviour
{
    [Header("DeckSelector")]
    [SerializeField] GameObject deckContainer;
    [SerializeField] GameObject deckPrefab;
    [SerializeField] GameObject newDeckSlotIcon;

    private List<GameObject> allDeckIcons = new List<GameObject>();
    
    [Header("References")]
    [SerializeField] GameObject deckSelector;
    [SerializeField] GameObject deckbuildingMenu;

    [Header("DeckBuilding")]
    [SerializeField] GameObject ownedCardContainer; 
    [SerializeField] GameObject deckCardContainer;
    List<GameObject> ownedCardList = new List<GameObject>();
    List<GameObject> deckCardList  = new List<GameObject>();
    [SerializeField] GameObject cardIconPrefab;
    public static int heldIndex;
    private AccountManager.Decks currentDeckData;
    

    private void Awake()
    {
        ResetDeckIcons();
    }

    public void SetUpDeckBuilder(int newIndex)
    {
        heldIndex = newIndex;

        deckSelector.SetActive(false);
        deckbuildingMenu.SetActive(true);

        while(deckCardList.Count > 0)
        {
            Destroy(deckCardList[0]);
            deckCardList.RemoveAt(0);
        }

        while(ownedCardList.Count > 0)
        {
            Destroy(ownedCardList[0]);
            ownedCardList.RemoveAt(0);
        }

        for(int i = 0; i < AccountManager.Instance.player.earnedItems.ownedCardTypes.Count; i++)
        {
            ownedCardList.Add(Instantiate(cardIconPrefab, transform.position, transform.rotation));
            ownedCardList[ownedCardList.Count - 1].transform.SetParent(ownedCardContainer.transform);
            ownedCardList[ownedCardList.Count - 1].GetComponent<CardIconScript>().SetUpCard(AccountManager.Instance.player.earnedItems.ownedCardsCount[i], AccountManager.Instance.player.earnedItems.ownedCardTypes[i]);

        }

        if(heldIndex < AccountManager.Instance.player.decks.Count)
        {
            //Load all of the details
        }
        else
        {
            currentDeckData = new AccountManager.Decks();

        }
    }

    public List<CardData> DecompileDeckString(string deck)
    {
        string[] cards = deck.Split(",");

        List<CardData> dataList = new List<CardData>();

        for(int i = 0; i < cards.Length; i++)
        {
            dataList.Add(Pals.ConvertToCardData(cards[i]));
        }

        return dataList;
    }

    public void SaveDeck()
    {
        if(heldIndex < AccountManager.Instance.player.decks.Count)
        {
            AccountManager.Instance.player.decks[heldIndex] = currentDeckData;
        }
        else
        {
            AccountManager.Instance.player.decks.Add(currentDeckData);
        }
    }

    public void ResetDeckIcons()
    {
        while(allDeckIcons.Count > 0)
        {
            Destroy(allDeckIcons[0]);
            allDeckIcons.RemoveAt(0);
        }

        for(int i = 0; i < AccountManager.Instance.player.decks.Count; i++)
        {
            allDeckIcons.Add(Instantiate(deckPrefab, transform.position, transform.rotation));
            allDeckIcons[i].transform.SetParent(deckContainer.transform);
            allDeckIcons[i].GetComponent<DeckIconButtonScript>().manager = this;
            allDeckIcons[i].GetComponent<DeckIconButtonScript>().indexNumber = i;
        }

        if(allDeckIcons.Count < 30)
        {
            allDeckIcons.Add(Instantiate(newDeckSlotIcon, transform.position, transform.rotation));
            allDeckIcons[allDeckIcons.Count - 1].transform.SetParent(deckContainer.transform);
            allDeckIcons[allDeckIcons.Count - 1].GetComponent<DeckIconButtonScript>().manager = this;
            allDeckIcons[allDeckIcons.Count - 1].GetComponent<DeckIconButtonScript>().indexNumber = allDeckIcons.Count - 1;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
