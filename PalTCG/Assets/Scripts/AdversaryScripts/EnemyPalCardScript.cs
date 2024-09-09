using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPalCardScript : MonoBehaviour
{
    protected Image image;
    public CardData cardData;
    public Color normalColor; //Probably Temp
    public Color targetColor;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void Hurt(int damage)
    {
        Debug.Log(damage);
    }
}
