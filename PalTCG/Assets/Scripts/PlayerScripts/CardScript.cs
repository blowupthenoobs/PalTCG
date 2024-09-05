using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public CardData cardData;
    private bool cancelSelect;
    public Color normalColor;
    public Color selectColor;
    private Image image;
    
    protected virtual void Awake()
    {
        cardData.gameObject = gameObject;
        cardData.Awake();
        image = cardData.image;
    }

    public void Select()
    {
        if(!cancelSelect && HandScript.Instance.state != "")
        {
            image.color = selectColor;
            HandScript.Instance.Select(gameObject);
        }
    }

    public virtual void Deselect()
    {
        cancelSelect = true;
        image.color = normalColor;
        StartCoroutine(ReAvaliablity());
    }

    public virtual IEnumerator ReAvaliablity()
    {
        yield return new WaitForSeconds(.15f);
        cancelSelect = false;
    }
}
