using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using DefaultUnitData;
using Resources;

public abstract class CardData : ScriptableObject
{
    public string cardID;
    public string CardName;
    public Sprite cardArt;
    public Image image;
    public GameObject gameObject;
    public Traits traits;
    public List<bool> usedAbilities = new List<bool>();
    public int maxHp;
    public int currentHp;
    public int currentAtk;

    
    public virtual void SetToGameObject()
    {
        image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    public virtual void Discard()
    {
        Destroy(gameObject);
    }
}
