using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData cardData;
    private bool cancelSelect;
    public Color normalColor;
    public Color selectColor;
    private Image image;

    protected virtual void Awake()
    {
        cardData.gameObject = gameObject;
        cardData.SetToGameObject();
        image = cardData.image;
    }

    public virtual void SetUpCard()
    {
        image.sprite = cardData.cardArt;
    }

    public void Select()
    {
        if (!cancelSelect && (HandScript.Instance.state == "default" || HandScript.Instance.state == "buildingPay"))
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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        HandScript.Instance.hoveredHandCard = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(HandScript.Instance.hoveredHandCard == gameObject)
            HandScript.Instance.hoveredHandCard = null;
    }
}
