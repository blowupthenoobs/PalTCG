using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using DefaultUnitData;

public class DiscardPileScript : MonoBehaviour
{
    [SerializeField] PhotonView opponentMirror;
    public DrawPileScript drawPile;
    public List<CardData> discardPile = new List<CardData>();

    public void DiscardCard(CardData newCard)
    {
        discardPile.Add(newCard);
        opponentMirror.RPC("AddToDiscardPile", RpcTarget.Others, newCard.cardID);
    }

    [PunRPC]
    public void AddToDiscardPile(string newCard)
    {
        discardPile.Add(Pals.ConvertToCardData(newCard));
    }

    public void Click()
    {
        GameManager.Instance.CardPileBox.GetComponent<CardListPopupScript>().CreateCardIcons(discardPile, true);
    }

    public void RemoveCard(CardData cardType)
    {
        discardPile.RemoveAt(discardPile.IndexOf(cardType));
    }
}
