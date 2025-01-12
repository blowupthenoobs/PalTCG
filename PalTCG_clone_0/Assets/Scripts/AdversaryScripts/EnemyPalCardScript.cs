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

    public void Select()
    {
        image.color = targetColor;
    }

    public void Deselect()
    {
        image.color = normalColor;
    }

    public void Hurt(int damage)
    {
        Debug.Log("took " + damage + " damage");
    }
    
    public void Rest()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    public void Wake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected void Die()
    {
        Debug.Log("unit is now dead :(");
    }

}
