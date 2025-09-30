using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCardScript : UnitCardScript
{
    public PhotonView photonAssigner;
    public void SetUpCard(string cardArt)
    {
        opponentMirror = photonAssigner;
        PlayerCardData data = (PlayerCardData)ScriptableObject.CreateInstance(typeof(PlayerCardData));
        data.gameObject = gameObject;
        data.SetUpData(cardArt, gameObject);
        health.text = data.currentHp.ToString();
        opponentMirror.RPC("UpdateHealth", RpcTarget.OthersBuffered, data.currentHp);
        opponentMirror.RPC("SetUpCard", RpcTarget.OthersBuffered, data.cardID);
        cardData = data;

        SetUpBasicTurnEvents();
    }

    protected override void Die()
    {
        GameManager.Instance.LoseGame();
        opponentMirror.RPC("OpponentDeath", RpcTarget.Others);
    }

    [PunRPC]
    public void HurtHeldCard(int damage, bool isAttacked)
    {
        Hurt(damage, isAttacked);
    }

    [PunRPC]
    public void EffectRest()
    {
        Rest();
    }
}
