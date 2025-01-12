using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using DefaultUnitData;

public class PlayerIconScript : MonoBehaviour
{
    private DeckBuildingManagerScript manager;
    [SerializeField] Image cardArt;
    [HideInInspector] public string cardDataReference;
    [HideInInspector] public int cardCount;
    public void SetUpCard(DeckBuildingManagerScript builder, string cardReference)
    {
        manager = builder;
        cardDataReference = cardReference;

        cardArt.sprite = Pals.ConvertToCardData(cardReference).cardArt;
    }

    public void Click()
    {
        manager.ChangePlayerCard(cardDataReference);
    }

    // public void InheritData(int Card)
    // {

    // }
}
