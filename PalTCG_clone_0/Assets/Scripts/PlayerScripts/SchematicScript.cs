using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchematicScript : MonoBehaviour
{
    public BuildingData cardData;
    private bool cancelSelect;
    public Color normalColor;
    public Color selectColor;
    private Image image;

    protected virtual void Awake()
    {
        // cardData.gameObject = gameObject;
        // cardData.SetToGameObject();
        image = gameObject.GetComponent<Image>();
    }

    public virtual void SetUpCard()
    {
        image.sprite = cardData.cardArt;
    }
    
    public void Select()
    {
        if(!cancelSelect && (HandScript.Instance.state == "default"))
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
