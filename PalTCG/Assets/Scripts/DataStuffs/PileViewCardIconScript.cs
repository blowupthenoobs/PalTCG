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
    private CardData cardData;
    private int currentCount;
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
    }
}
