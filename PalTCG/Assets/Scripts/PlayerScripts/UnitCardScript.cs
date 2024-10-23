using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardScript : MonoBehaviour
{
    protected Image image;
    protected Button button;
    public CardData cardData;
    public Color normalColor; //Probably Temp
    public Color selectColor;

    //Effects and state variables
    public bool isResting;


    void Awake()
    {
        button = GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
    }

    public void SetUpCard(CardData newData)
    {
        cardData = newData;
        image.sprite = cardData.cardArt;
    }
    
    public void Hurt(int dmg)
    {
        cardData.currentHp -= dmg;

        if(cardData.currentHp < 0)
            Die();
    }

    public void Heal(int heal)
    {
        cardData.currentHp += heal;

        if(cardData.currentHp > cardData.maxHp)
            cardData.currentHp = cardData.maxHp;
    }

    public void Rest()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        isResting = true;
    }

    public void Wake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isResting = false;
    }

    protected void Die()
    {
        Debug.Log("unit is now dead :(");
    }
}
