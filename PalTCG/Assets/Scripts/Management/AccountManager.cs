using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultUnitData;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;
    public Sprites CardSprites = new Sprites();
    [HideInInspector] public PalAbilitySets PalAbilities = new PalAbilitySets();

    public Account player;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if(ES3.KeyExists("account"))
            player = ES3.Load<Account>("account");
        else
        {
            Debug.Log("created new account");
            player = new Account();
        }

        ToolSlotScript.allToolSlots.Clear();
    }

    private void OnApplicationQuit()
    {
        ES3.Save("account", player);
    }

#region structs
    [System.Serializable]
    public struct Account
    {
        public string accountName;
        public Stats stats;
        public Settings settings;
        public EarnedItems earnedItems;
        public List<string> achivements;
        public List<Decks> decks;

        public Account(string accountName, Stats stats, Settings settings, EarnedItems earnedItems, List<string> achievements, List<Decks> decks)
        {
            this.accountName = accountName;
            this.stats = stats;
            this.settings = settings;
            this.earnedItems = earnedItems;
            this.achivements = achievements;
            this.decks = decks;
        }
    }

    [System.Serializable]
    public struct Stats
    {
        public int games;
        public int wins;
        public int losses;
        public Stats(int games, int wins, int losses)
        {
            this.games = games;
            this.wins = wins;
            this.losses = losses;
        }
    }

    
    [System.Serializable]
    public struct Settings
    {
        public float masterVol;
        public float musicVol;
        public float sfxVol;
        public bool useKeyboardShortCuts;
    }

    [System.Serializable]
    public struct EarnedItems
    {
        public List<string> ownedCardTypes;
        public List<int> ownedCardsCount;
        public List<string> ownedPlayerCards;
        public List<string> ownedFrames;

        public EarnedItems(List<string> ownedCardTypes, List<int> ownedCardsCount, List<string> ownedPlayerCards, List<string> ownedFrames)
        {
            this.ownedCardTypes = ownedCardTypes;
            this.ownedCardsCount = ownedCardsCount;
            this.ownedPlayerCards = ownedPlayerCards;
            this.ownedFrames = ownedFrames;
        }
    }

    [System.Serializable]
    public struct Decks
    {
        public string deckName;
        public string coverCard;
        public string playerCard;
        public string decklist;
        public bool isLegal;
        public Decks(string deckName, string coverCard, string playerCard, string decklist, bool isLegal)
        {
            this.deckName = deckName;
            this.coverCard = coverCard;
            this.playerCard = playerCard;
            this.decklist = decklist;
            this.isLegal = isLegal;
        }
    }

#endregion structs
}
