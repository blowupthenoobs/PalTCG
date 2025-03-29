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

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
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
        transform.parent.GetComponent<EnemyPalSphereScript>().opponentMirror.RPC("HurtHeldCard", RpcTarget.Others, damage);
        // Debug.Log("took " + damage + " damage");
    }
    
    public void Rest()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    public void Wake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected void Die()
    {
        Debug.Log("unit is now dead :(");
    }

    public void AfterBlockActions()
    {
        opponentMirror.RPC("AfterBlockActions", RpcTarget.Others);
    }

    public void SetMirror(PhotonView view)
    {
        opponentMirror = view;
    }
}
