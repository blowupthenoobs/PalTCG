using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    # region CardDraw
    public List<CardScript> deck = new List<CardScript>();
    public Transform[] cardSlots;
    public bool[] availableSlots;

    public Text deckSizeText;

    public void DrawCard()
    {
        if(deck.Count >= 1)
        {
            CardScript randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableSlots.Length; i++)
            {
                if(availableSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }
    #endregion

    #region Singleton
    // Make sure only one instance of the Game Manager exists.
    private static GameManager _instance;

    // Make access limited when loading.
    public static GameManager Instance
    {
        get 
        { 
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL.");
            }
            return _instance; 
        }
    }

    // Check to see if another Game Manager exists. If another exists, destory the Game Object.
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        // If no other exists, don't destory the original.
        DontDestroyOnLoad(this);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();
    }
}
