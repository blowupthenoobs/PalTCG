using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DefaultUnitData;

public class DeckSelectorIconScript : MonoBehaviour
{
    [HideInInspector] public GamePrepManagerScript manager;
    [HideInInspector] public int indexNumber;
    [SerializeField] Image image;
    [SerializeField] Color normalColor;
    [SerializeField] Color selectColor;

    public void SetUpIcon()
    {
        image.sprite = Pals.ConvertToCardData(AccountManager.Instance.player.decks[indexNumber].coverCard).cardArt;
    }

    public void Click()
    {
        manager.SendMessage("SelectDeck", indexNumber);
    }

    public void Select()
    {
        image.color = selectColor;
    }

    public void Deselect()
    {
        image.color = normalColor;
    }
}
