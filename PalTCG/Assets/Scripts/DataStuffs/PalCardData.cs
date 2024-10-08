using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DefaultUnitData;

[CreateAssetMenu(menuName = "Card/PalCard")]
public class PalCardData : CardData
{
    public PalData orignalData;
    public int cost;
    public Resources.Element element;
    public Resources.Traits traits;
    public int size;

    public void DecomposeData(PalData newData)
    {
        orignalData = newData;

        cardArt = orignalData.cardArt;
        traits = orignalData.traits;
        size = orignalData.size;
        cost = orignalData.cost;
        element = orignalData.element;
    }
}
