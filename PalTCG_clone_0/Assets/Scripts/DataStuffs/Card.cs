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
    public int maxHp;
    public int currentHp;
    public int currentAtk;

    //PalCard Stuffs
    public List<UnityAction> OnDestroy = new List<UnityAction>();
    public List<UnityAction> WhenAttack = new List<UnityAction>();
    public List<UnityAction> OnAttack = new List<UnityAction>();
    public List<UnityAction> OnHurt = new List<UnityAction>();
    public List<UnityAction> OnPlay = new List<UnityAction>();
    public List<UnityAction> EndOfTurn = new List<UnityAction>();
    public List<UnityAction> OncePerTurn = new List<UnityAction>();
    public List<int> AbilityUseCounter = new List<int>();

    public List<UnityAction> PalSkill = new List<UnityAction>();
    public List<UnityAction> WhenSkillAttack = new List<UnityAction>();
    public List<UnityAction> OnSkillAttack = new List<UnityAction>();
    public List<UnityAction> EndOfSkillTurn = new List<UnityAction>();

    
    public virtual void SetToGameObject()
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
}
