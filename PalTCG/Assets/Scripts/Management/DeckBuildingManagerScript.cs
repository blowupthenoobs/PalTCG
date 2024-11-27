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
    [SerializeField] GameObject highlightCardContainer;
    [SerializeField] GameObject chosenPlayerCardContainer;
    [SerializeField] GameObject allPlayerCardsContainer;
    [SerializeField] GameObject ownedCardContainer; 
    [SerializeField] GameObject deckCardContainer;
    List<GameObject> ownedPlayerCards = new List<GameObject>();
    List<GameObject> ownedCardList = new List<GameObject>();
    List<GameObject> deckCardList  = new List<GameObject>();
    List<string> rawCardList = new List<string>();
    [SerializeField] GameObject playerCardIconPrefab;
    [SerializeField] GameObject cardIconPrefab;
    public static int heldIndex;
    private AccountManager.Decks currentDeckData;
    public bool changingMainCard;
    

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

        while(ownedPlayerCards.Count > 0)
        {
            Destroy(ownedPlayerCards[0]);
            ownedPlayerCards.RemoveAt(0);
        }

        for(int i = 0; i < AccountManager.Instance.player.earnedItems.ownedCardTypes.Count; i++)
        {
            ownedCardList.Add(Instantiate(cardIconPrefab, transform.position, transform.rotation));
            ownedCardList[ownedCardList.Count - 1].transform.SetParent(ownedCardContainer.transform);
            ownedCardList[ownedCardList.Count - 1].GetComponent<CardIconScript>().SetUpCard(this, AccountManager.Instance.player.earnedItems.ownedCardsCount[i], AccountManager.Instance.player.earnedItems.ownedCardTypes[i], true, artShowcase, textShowcase);
            ownedCardList[ownedCardList.Count - 1].GetComponent<CardIconScript>().isFromFullList = true;
        }

        for(int i = 0; i < AccountManager.Instance.player.earnedItems.ownedPlayerCards.Count; i++)
        {
            ownedPlayerCards.Add(Instantiate(playerCardIconPrefab, transform.position, transform.rotation));
            ownedPlayerCards[ownedPlayerCards.Count - 1].transform.SetParent(allPlayerCardsContainer.transform);
            ownedPlayerCards[ownedPlayerCards.Count - 1].GetComponent<PlayerIconScript>().SetUpCard(this, AccountManager.Instance.player.earnedItems.ownedPlayerCards[i]);
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
            
            ChangeMainCard(currentDeckData.coverCard);
            ChangePlayerCard(currentDeckData.playerCard);
                
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

                if(currentDeckData.coverCard == rawCardRef)
                    ChangeMainCard(currentDeckData.playerCard);
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

    public void ChangeDeckName(string newName)
    {
        currentDeckData.deckName = newName;
    }

    public void LookForNewDeckHighlight()
    {
        changingMainCard = true;
    }

    public void ChangeMainCard(string cardReference)
    {
        changingMainCard = false;
        currentDeckData.coverCard = cardReference;
        highlightCardContainer.GetComponent<Image>().sprite = Pals.ConvertToCardData(cardReference).cardArt;
    }

    public void ChangeHighlightCardToPlayer()
    {
        if(changingMainCard)
        {
            changingMainCard = false;
            currentDeckData.coverCard = currentDeckData.playerCard;
            highlightCardContainer.GetComponent<Image>().sprite = Pals.ConvertToCardData(currentDeckData.playerCard).cardArt;
        }
    }

    public void ChangePlayerCard(string playerReference)
    {
        currentDeckData.playerCard = playerReference;
        chosenPlayerCardContainer.GetComponent<Image>().sprite = Pals.ConvertToCardData(playerReference).cardArt;
    }

    public void SaveDeck()
    {
        currentDeckData.decklist = "";

        foreach(GameObject icon in deckCardList)
        {
            for(int i = 0; i < icon.GetComponent<CardIconScript>().cardCount; i++)
            {
                currentDeckData.decklist = currentDeckData.decklist + "," + rawCardList[deckCardList.IndexOf(icon)];
            }
        }

        currentDeckData.decklist = currentDeckData.decklist.Substring(1);

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
            allDeckIcons[i].SendMessage("SetUpIcon");
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
