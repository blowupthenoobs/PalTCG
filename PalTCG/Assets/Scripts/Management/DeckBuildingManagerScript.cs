using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] TMP_InputField deckNameSpot;
    [SerializeField] GameObject artShowcase;
    [SerializeField] GameObject textShowcase;
    [SerializeField] GameObject ownedCardContainer; 
    [SerializeField] GameObject deckCardContainer;
    public List<GameObject> ownedCardList = new List<GameObject>(); //Only public for sake of debug
    public List<GameObject> deckCardList  = new List<GameObject>();
    public List<string> rawCardList = new List<string>();
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
            rawCardList.RemoveAt(0);
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
            ownedCardList[ownedCardList.Count - 1].GetComponent<CardIconScript>().SetUpCard(this, AccountManager.Instance.player.earnedItems.ownedCardsCount[i], AccountManager.Instance.player.earnedItems.ownedCardTypes[i], true, artShowcase, textShowcase);
            ownedCardList[ownedCardList.Count - 1].GetComponent<CardIconScript>().isFromFullList = true;
        }

        if(heldIndex < AccountManager.Instance.player.decks.Count)
        {
            currentDeckData = AccountManager.Instance.player.decks[newIndex];

            deckNameSpot.text = currentDeckData.deckName;
            
            if(AccountManager.Instance.player.decks[newIndex].decklist != "")
            {
                string[] temp = AccountManager.Instance.player.decks[newIndex].decklist.Split(",");;

                for(int i = 0; i < temp.Length; i++)
                {
                    if(rawCardList.Contains(temp[i]))
                    {
                        deckCardList[rawCardList.IndexOf(temp[i])].SendMessage("Increment", 1);
                    }
                    else
                    {
                        rawCardList.Add(temp[i]);
                        deckCardList.Add(Instantiate(cardIconPrefab, transform.position, transform.rotation));
                        deckCardList[deckCardList.Count - 1].transform.SetParent(deckCardContainer.transform);
                        deckCardList[deckCardList.Count - 1].GetComponent<CardIconScript>().SetUpCard(this, 1, temp[i], false, artShowcase, textShowcase);
                    }
                }
            }
                
        }
        else
        {
            currentDeckData = new AccountManager.Decks();
        }
    }

    public void SwitchIconSide(bool isFromFullList, string rawCardRef)
    {
        if(isFromFullList)
        {
            if(rawCardList.Contains(rawCardRef))
            {
                if(deckCardList[rawCardList.IndexOf(rawCardRef)].GetComponent<CardIconScript>().cardCount < AccountManager.Instance.player.earnedItems.ownedCardsCount[AccountManager.Instance.player.earnedItems.ownedCardTypes.IndexOf(rawCardRef)])
                    deckCardList[rawCardList.IndexOf(rawCardRef)].SendMessage("Increment", 1);
            }
            else
            {
                rawCardList.Add(rawCardRef);
                deckCardList.Add(Instantiate(cardIconPrefab, transform.position, transform.rotation));
                deckCardList[deckCardList.Count - 1].transform.SetParent(deckCardContainer.transform);
                deckCardList[deckCardList.Count - 1].GetComponent<CardIconScript>().SetUpCard(this, 1, rawCardRef, false, artShowcase, textShowcase);
            }
        }
        else
        {
            deckCardList[rawCardList.IndexOf(rawCardRef)].SendMessage("Increment", -1);

            if(deckCardList[rawCardList.IndexOf(rawCardRef)].GetComponent<CardIconScript>().cardCount <= 0)
            {
                var index = rawCardList.IndexOf(rawCardRef);

                rawCardList.RemoveAt(index);
                Destroy(deckCardList[index]);
                deckCardList.RemoveAt(index);
            }
        }
    }

    public List<string> DecompileDeckString(string deck)
    {
        string[] cards = deck.Split(",");

        List<string> dataList = new List<string>();

        for(int i = 0; i < cards.Length; i++)
        {
            dataList.Add(cards[i]);
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
