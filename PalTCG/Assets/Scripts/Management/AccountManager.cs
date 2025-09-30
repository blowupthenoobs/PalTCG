using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultUnitData;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;
    public Sprites CardSprites = new Sprites();

    public Account player;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (ES3.KeyExists("account"))
            player = ES3.Load<Account>("account");
        else
        {
            Debug.Log("created new account");
            player = new Account();
            TempGiveStartingCards();
        }

        ToolSlotScript.allToolSlots.Clear();
    }

    private void OnApplicationQuit()
    {
        ES3.Save("account", player);
    }

#region structs
    [System.Serializable]
    public class Account
    {
        public string accountName;
        public Stats stats;
        public Settings settings;
        public EarnedItems earnedItems;
        public List<string> achievements;
        public List<Decks> decks;

        public Account(string accountName, Stats stats, Settings settings, EarnedItems earnedItems, List<string> achievements, List<Decks> decks)
        {
            this.accountName = accountName;
            this.stats = stats;
            this.settings = settings;
            this.earnedItems = earnedItems;
            this.achievements = achievements;
            this.decks = decks;
        }

        public Account()
        {
            this.accountName = "new account";
            this.stats = new Stats();
            this.settings = new Settings();
            this.earnedItems = new EarnedItems();
            this.achievements = new List<string>();
            this.decks = new List<Decks>();
        }
    }

    [System.Serializable]
    public struct Stats
    {
        public int games;
        public int wins;
        public int losses;
        public Stats(int games = 0, int wins = 0, int losses = 0)
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

        public Settings(float masterVol = 100, float musicVol = 100, float sfxVol = 50, bool useKeyboardShortCuts = true)
        {
            this.masterVol = masterVol;
            this.musicVol = musicVol;
            this.sfxVol = sfxVol;
            this.useKeyboardShortCuts = useKeyboardShortCuts;
        }
    }

    [System.Serializable]
    public class EarnedItems
    {
        public List<string> ownedCardTypes;
        public List<string> ownedBuildingTypes;
        public List<int> ownedCardsCount;
        public List<string> ownedPlayerCards;
        public List<string> ownedFrames;

        public EarnedItems(List<string> ownedCardTypes, List<string> ownedBuildingTypes, List<int> ownedCardsCount, List<string> ownedPlayerCards, List<string> ownedFrames)
        {
            // ownedCardTypes ??= new List<string>();
            // ownedBuildingTypes ??= new List<string>();
            // ownedCardsCount ??= new List<int>();
            // ownedPlayerCards ??= new List<string>();
            // ownedFrames ??= new List<string>();

            this.ownedCardTypes = ownedCardTypes;
            this.ownedBuildingTypes = ownedBuildingTypes;
            this.ownedCardsCount = ownedCardsCount;
            this.ownedPlayerCards = ownedPlayerCards;
            this.ownedFrames = ownedFrames;
        }

        public EarnedItems()
        {
            this.ownedCardTypes = new List<string>();
            this.ownedBuildingTypes = new List<string>();
            this.ownedCardsCount = new List<int>();
            this.ownedPlayerCards = new List<string>();
            this.ownedFrames = new List<string>();
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

    private void TempGiveStartingCards()
    {
        player.earnedItems.ownedPlayerCards.Add("h/playerDef");
        player.earnedItems.ownedPlayerCards.Add("h/playerAlt");
        player.earnedItems.ownedPlayerCards.Add("h/normalAxel");
        player.earnedItems.ownedPlayerCards.Add("h/normalZoe");
        player.earnedItems.ownedPlayerCards.Add("h/overheadZoe");

        player.earnedItems.ownedCardTypes.Add("p/lamball/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/cattiva/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/chikipi/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/daedream/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/depresso/0");
        player.earnedItems.ownedCardsCount.Add(2);
        player.earnedItems.ownedCardTypes.Add("p/depresso/1");
        player.earnedItems.ownedCardsCount.Add(1);
        player.earnedItems.ownedCardTypes.Add("p/foxsparks/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/sparkit/0");
        player.earnedItems.ownedCardsCount.Add(2);
        player.earnedItems.ownedCardTypes.Add("p/rooby/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/fuddler/0");
        player.earnedItems.ownedCardsCount.Add(2);
        player.earnedItems.ownedCardTypes.Add("p/dumud/0");
        player.earnedItems.ownedCardsCount.Add(2);
        player.earnedItems.ownedCardTypes.Add("p/incineram/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("p/flambell/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("t/pickaxe/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("t/axe/0");
        player.earnedItems.ownedCardsCount.Add(3);
        player.earnedItems.ownedCardTypes.Add("t/saddle/0");
        player.earnedItems.ownedCardsCount.Add(1);

        player.earnedItems.ownedBuildingTypes.Add("feedingBox");
    }
}
