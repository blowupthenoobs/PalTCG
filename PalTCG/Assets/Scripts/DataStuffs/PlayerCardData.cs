using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;
using DefaultUnitData;
public class PlayerCardData : CardData
{
    public void SetUpData(string ID, GameObject host)
    {
        cardID = ID;
        CardName = "player";
        string[] dataParts = ID.Split("/");
        cardArt = Pals.LookForPlayerArt(dataParts[1]);
        gameObject = host;
        SetToGameObject();
        traits = new Traits();
        maxHp = 30;
        currentHp = maxHp;
        currentAtk = 1;

        image.sprite = cardArt;
    }
}
