using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileScript : MonoBehaviour
{
    public new List<GameObject> drawPile = new List<GameObject>();
    public new List<string> cardData = new List<string>();

    public void Draw()
    {
        if(drawPile.Count > 0)
        {
            GameObject newCard = Instantiate(drawPile[0], transform.position, transform.rotation);
            newCard.SendMessage("SetPrefab", drawPile[0]);

            HandScript.Instance.Draw(newCard);
            newCard.SendMessage("DecomposeData", cardData[0]);

            drawPile.RemoveAt(0);
            cardData.RemoveAt(0);
        }
        else
            Debug.Log("Draw pile empty");
    }
}
