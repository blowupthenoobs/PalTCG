using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DefaultUnitData;

public class PileViewCardIconScript : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text counter;
    [HideInInspector] public CardData cardData;
    private int currentCount;

    [SerializeField] Color normalColor;
    [SerializeField] Color selectColor;

    void Start()
    {
        icon = GetComponent<Image>();
    }

    public void SetUpIcon(CardData data, int count)
    {
        cardData = data;
        icon.sprite = cardData.cardArt;
        currentCount = count;
        counter.text = currentCount.ToString();

        GetComponent<Button>().interactable = CardListPopupScript.lookingAtDiscard && HandScript.Instance.state == "choosingCardInDiscard";

        if(HandScript.Instance.tempDataTypeRef == cardData && CardListPopupScript.lookingAtDiscard && HandScript.Instance.state == "choosingCardInDiscard")
            Select();
    }

    public void Click()
    {
        if(CardListPopupScript.lookingAtDiscard && HandScript.Instance.state == "choosingCardInDiscard")
            HandScript.Instance.Select(gameObject);
        else
            GetComponent<Button>().interactable = false;
    }

    public void Select()
    {
        icon.color = selectColor;
    }

    public void Deselect()
    {
        icon.color = normalColor;
    }
}
