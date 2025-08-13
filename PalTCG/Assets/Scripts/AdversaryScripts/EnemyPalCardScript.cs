using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class EnemyPalCardScript : MonoBehaviour
{
    public PhotonView opponentMirror;
    protected Image image;
    public CardData cardData;
    public Color normalColor; //Probably Temp
    public Color targetColor;
    public TMP_Text health;
    public GameObject heldCard;

    public bool hovered;
    private bool viewButtonPressed;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        CheckForAltPress();

        if (viewButtonPressed && hovered)
            LargeCardViewScript.Instance.FocusCard(cardData.cardArt, gameObject);

    }

    public void SetUpCard(CardData newData)
    {
        cardData = newData;
        image.sprite = cardData.cardArt;
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
        opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, damage);
    }

    public void UpdateHealth(int newHealth)
    {
        if (heldCard == null)
        {
            cardData.currentHp = newHealth;
            health.text = newHealth.ToString();
        }
        else
            heldCard.SendMessage("UpdateHealth", newHealth);
    }

    public void SendRestEffect()
    {
        opponentMirror.RPC("Rest", RpcTarget.Others);
    }

    public void Rest()
    {
        if (heldCard == null)
            transform.rotation = Quaternion.Euler(0, 0, -90);
        else
            heldCard.SendMessage("Rest");
    }

    public void Wake()
    {
        if (heldCard == null)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            heldCard.SendMessage("Wake");
    }

    public void Die()
    {
        if (heldCard == null)
            Destroy(gameObject);
        else
            heldCard.SendMessage("Die");
    }

    public void AfterBlockActions()
    {
        opponentMirror.RPC("AfterBlockActions", RpcTarget.Others);
    }

    public void SetMirror(PhotonView view)
    {
        opponentMirror = view;
    }

    public void StackCard(GameObject CardToStack)
    {
        heldCard = CardToStack;
        heldCard.transform.SetParent(transform);
        heldCard.transform.position = transform.position;
        heldCard.SendMessage("SetMirror", opponentMirror);
        HideHealthCounter();
    }

    public void HideHealthCounter()
    {
        health.text = "";
    }

    private void CheckForAltPress()
    {
        viewButtonPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);

        if((Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt)) && !viewButtonPressed)
        {
            if(!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
                LargeCardViewScript.Instance.CloseZoom(gameObject);
        }
    }
}
