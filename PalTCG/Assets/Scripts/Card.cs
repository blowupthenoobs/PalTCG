using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class CardData : ScriptableObject
{
    //First Script
    public string CardType;
    public string CardName;
    public Sprite cardart;
    public int Cost;
    public string Ability;
    private GameObject gameObject;
    private bool cancelSelect;
    public Color normalColor;
    public Color selectColor;
    private UnityEngine.UI.Image image;

    public virtual void Awake()
    {
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    public void Select()
    {
        if(!cancelSelect)
        {
            image.color = selectColor;
            HandScript.Instance.Select(gameObject);
        }
    }

    public virtual void Deselect()
    {
        cancelSelect = true;
        image.color = normalColor;
        // StartCoroutine(ReAvaliablity());
    }

    public virtual IEnumerator ReAvaliablity()
    {
        yield return new WaitForSeconds(.15f);
        cancelSelect = false;
    }

    public virtual void Discard()
    {
        Destroy(gameObject);
    }

    public virtual void Effect(GameObject target)
    {
        Debug.Log("No Effect Set");
    }

    public virtual void DecomposeData(string data)
    {
        Debug.Log("No decompesition set for this item");
    }
}
