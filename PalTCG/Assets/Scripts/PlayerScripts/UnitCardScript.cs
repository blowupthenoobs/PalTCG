using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardScript : MonoBehaviour
{
    protected Image image;
    protected Button button;
    public CardData cardData;
    public Color normalColor; //Probably Temp
    public Color selectColor;

    //Effects and state variables
    public bool resting;


    void Awake()
    {
        button = GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
    }

    public void SetUpCard(CardData newData)
    {
        cardData = newData;
        image.sprite = cardData.cardArt;
    }
    
    public void Hurt(int dmg)
    {
        cardData.currentHp -= dmg;

        if(cardData.currentHp < 0)
            Die();
    }

    public void Heal(int heal)
    {
        cardData.currentHp += heal;

        if(cardData.currentHp > cardData.maxHp)
            cardData.currentHp = cardData.maxHp;
    }

    public void PrepareAttackPhase()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(SelectForAttack);
    }

    public void Attack(GameObject target)
    {
        if(cardData.WhenAttack != null)
            cardData.WhenAttack.Invoke();
        
        target.SendMessage("Hurt", cardData.currentAtk);

        if(cardData.OnAttack != null)
            cardData.OnAttack.Invoke();
    }
    public void EndAttackPhase()
    {
        button.onClick.RemoveAllListeners();
    }

    public void SelectForAttack()
    {
        if(!resting)
        {
            if(HandScript.Instance.state == "choosingAttack" || HandScript.Instance.state == "targeting")
            {
                if(HandScript.Instance.selected != gameObject && !HandScript.Instance.selection.Contains(gameObject))
                {
                    image.color = selectColor;
                    HandScript.Instance.Select(gameObject);
                }
                else
                    RemoveFromSelection(); 
            }
            else if(HandScript.Instance.state == "raiding")
            {
                if(HandScript.Instance.selected != gameObject && !HandScript.Instance.selection.Contains(gameObject))
                {
                    image.color = selectColor;
                    HandScript.Instance.Select(gameObject);
                }
                else
                    RemoveFromSelection(); 
            }
            else
                RemoveFromSelection();
        }
    }

    public void RemoveFromSelection()
    {
        if(HandScript.Instance.selected == gameObject)
        {
            HandScript.Instance.selected = null;

            if(HandScript.Instance.state == "targeting")
                HandScript.Instance.state = "choosingAttack";
        }
        if(HandScript.Instance.selection.Contains(gameObject))
            HandScript.Instance.selection.Remove(gameObject);

        image.color = normalColor;
    }


    public void Rest()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        resting = true;
    }

    public void Wake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        resting = false;
    }

    protected void Die()
    {
        Debug.Log("unit is now dead :(");
    }

    public void Deselect()
    {
        image.color = normalColor;
    }
}
