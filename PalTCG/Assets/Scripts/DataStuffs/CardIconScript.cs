using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DefaultUnitData;

public class CardIconScript : MonoBehaviour
{
    [HideInInspector] CardData data;
    [SerializeField] TMP_Text TextBox;
    [SerializeField] Image cardArt;
    private string cardDataReference;
    public void SetUpCard(int cardCount, string cardReference)
    {
        data = Pals.ConvertToCardData(cardReference);
        TextBox.text = cardCount.ToString();
        cardDataReference = cardReference;

        cardArt.sprite = data.cardArt;
    }
}
