using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using Resources;

public class CardHolderScript : MonoBehaviour
{

    [SerializeField] protected PhotonView opponentMirror;
    [SerializeField] protected GameObject waitingSpace;
    [SerializeField] protected GameObject cardPrefab;
    public GameObject heldCard;

#region cardDelegation
    [PunRPC]
    public void Rest()
    {
        heldCard.SendMessage("Rest");
    }

    [PunRPC]
    public void HurtHeldCard(int damage, bool isAttacked = true)
    {
        heldCard.GetComponent<UnitCardScript>().Hurt(damage, isAttacked);
    }

    [PunRPC]
    public void AfterBlockActions()
    {
        heldCard.SendMessage("AfterBlockActions");
    }

    [PunRPC]
    public void GainTokens(string tokenType, int tokenCount)
    {
        heldCard.GetComponent<UnitCardScript>().GainTokens(tokenType, tokenCount);
    }

    [PunRPC]
    public void GetShocked()
    {
        heldCard.GetComponent<UnitCardScript>().StartCoroutine("GetShocked");
    }

    [PunRPC]
    public void ShockOtherCard()
    {
        heldCard.SendMessage("ShockOtherCard");
    }

    public void PrepareCardForMoving()
    {
        heldCard = null;
        opponentMirror.RPC("PrepareCardForMoving", RpcTarget.Others);
    }
    #endregion
}
