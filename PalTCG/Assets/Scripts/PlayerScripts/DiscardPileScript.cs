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
    
    public void Restock()
    {
        // while(discardPile.Count > 0)
        // {
        //     int index = Random.Range(0, discardPile.Count);

        //     drawPile.drawPile.Add(discardPile[index]);
        //     drawPile.cardData.Add(cardData[index]);

        //     discardPile.RemoveAt(index);
        //     cardData.RemoveAt(index);
        // }
    }
}
