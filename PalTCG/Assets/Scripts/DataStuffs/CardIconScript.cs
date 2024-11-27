using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

using DefaultUnitData;

public class CardIconScript : MonoBehaviour, IPointerEnterHandler
{
    private DeckBuildingManagerScript manager;
    private GameObject cardShowcase;
    private GameObject abilityShowcase;
    [HideInInspector] CardData data;
    [SerializeField] TMP_Text textBox;
    [SerializeField] Image cardArt;
    [HideInInspector] public string cardDataReference;
    [HideInInspector] public int cardCount;
    [HideInInspector] public bool isFromFullList;
    public void SetUpCard(DeckBuildingManagerScript builder, int totalCount, string cardReference, bool inOwnedList, GameObject artShowcase, GameObject textShowcase)
    {
        manager = builder;
        data = Pals.ConvertToCardData(cardReference);
        cardCount = totalCount;
        textBox.text = cardCount.ToString();
        cardDataReference = cardReference;
        isFromFullList = inOwnedList;
        cardShowcase = artShowcase;
        abilityShowcase = textShowcase;

        cardArt.sprite = data.cardArt;
    }

    public void Click()
    {
        if(!manager.changingMainCard)
            manager.SwitchIconSide(isFromFullList, cardDataReference);
        else if(!isFromFullList)
            manager.ChangeMainCard(cardDataReference);
    }

    public void Increment(int amount)
    {
        cardCount += amount;
        textBox.text = cardCount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardShowcase.GetComponent<Image>().sprite = cardArt.sprite;
    }



    // public void InheritData(int Card)
    // {

    // }
}
