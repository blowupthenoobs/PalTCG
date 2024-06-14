using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CardData : ScriptableObject
{
    public string CardType;
    public string CardName;
    public Sprite cardart;
    public int Cost;
    public string Ability;
}
