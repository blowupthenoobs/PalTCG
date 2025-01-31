using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DefaultUnitData;

public class GamePrepManagerScript : MonoBehaviour
{
    List<GameObject> deckIcons = new List<GameObject>();
    [SerializeField] GameObject deckContainer;
    [SerializeField] GameObject deckPrefab;
    [SerializeField] GameObject drawPile;
    private int currentSelectedDeckIndex;
    private bool lockedIn;

    // Start is called before the first frame update
    void Start()
    {
        SetUpDeckIcons();
    }

    public void SelectDeck(int indexNumber)
    {
        if(!lockedIn)
        {
            deckIcons[currentSelectedDeckIndex].SendMessage("Deselect");
            deckIcons[indexNumber].SendMessage("Select");
            currentSelectedDeckIndex = indexNumber;
        }
    }

    public void LockInDeck()
    {
        lockedIn = true;
        drawPile.SendMessage("SetDeckCards", AccountManager.Instance.player.decks[currentSelectedDeckIndex].decklist);
        GameManager.Instance.CardPileBox.SendMessage("");
    }

    private void SetUpDeckIcons()
    {
        for(int i = 0; i < AccountManager.Instance.player.decks.Count; i++)
        {
            deckIcons.Add(Instantiate(deckPrefab, transform.position, transform.rotation));
            deckIcons[i].transform.SetParent(deckContainer.transform);
            deckIcons[i].GetComponent<DeckSelectorIconScript>().manager = this;
            deckIcons[i].GetComponent<DeckSelectorIconScript>().indexNumber = i;
            deckIcons[i].SendMessage("SetUpIcon");
        }
    }
}
