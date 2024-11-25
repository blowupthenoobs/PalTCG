using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    public Account player;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

#region structs
    [System.Serializable]
    public struct Account
    {
        public string accountName;
        public Stats stats;
        public List<string> achivements;
        public List<Decks> decks;

        public Account(string accountName, Stats stats, List<string> achievements, List<Decks> decks)
        {
            this.accountName = accountName;
            this.stats = stats;
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
        public List<object> ownedCards;
        public List<string> ownedPlayerCards;
        public List<string> ownedFrames;

        public EarnedItems(List<object> ownedCards, List<string> ownedPlayerCards, List<string> ownedFrames)
        {
            this.ownedCards = ownedCards;
            this.ownedPlayerCards = ownedPlayerCards;
            this.ownedFrames = ownedFrames;
        }
    }

    [System.Serializable]
    public struct Decks
    {
        public string playerCard;
        public string playerFrame;
        public string decklist;

        public Decks(string playerCard, string playerFrame, string decklist)
        {
            this.playerCard = playerCard;
            this.playerFrame = playerFrame;
            this.decklist = decklist;
        }
    }

#endregion structs
}
