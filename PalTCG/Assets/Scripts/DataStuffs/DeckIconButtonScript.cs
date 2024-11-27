using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using DefaultUnitData;

public class DeckIconButtonScript : MonoBehaviour
{
    [HideInInspector] public DeckBuildingManagerScript manager;
    [HideInInspector] public int indexNumber;
    [SerializeField] Image image;
    [SerializeField] TMP_Text textbox;

    public void SetUpIcon()
    {
        image.sprite = Pals.ConvertToCardData(AccountManager.Instance.player.decks[indexNumber].coverCard).cardArt;
        textbox.text = AccountManager.Instance.player.decks[indexNumber].deckName;
    }

    public void Click()
    {
        manager.SendMessage("SetUpDeckBuilder", indexNumber);
    }
}
