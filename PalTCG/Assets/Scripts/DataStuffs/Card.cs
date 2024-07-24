using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class CardData : ScriptableObject
{
    public string CardType;
    public string CardName;
    public Sprite cardart;
    public UnityEngine.UI.Image image;
    public GameObject gameObject;

    
    public virtual void Awake()
    {
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
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
