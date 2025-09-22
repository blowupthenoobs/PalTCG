using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using DefaultUnitData;

[CreateAssetMenu(menuName = "Card/PalCard")]
public class PalCardData : CardData
{
    public PalData originalData;
    public int cost;
    public Resources.Element element;
    public int size;
    public string palSkill;

    public void DecomposeData(PalData newData)
    {
        originalData = newData;

        cardArt = originalData.cardArt;
        traits = originalData.traits;
        size = originalData.size;
        cost = originalData.cost;
        element = originalData.element;
        currentAtk = originalData.attackPower;
        maxHp = originalData.maxHp;
        cardID = originalData.cardID;

        palSkill = cardID.Split("/")[1];

        CreateUsedAbilityTrackers();
    }

    public override bool Equals(object obj)
    {
        if(obj is PalCardData otherData)
        {
            return originalData == otherData.originalData;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return originalData.GetHashCode();
    }

   public static bool operator ==(PalCardData left, PalCardData right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PalCardData left, PalCardData right)
    {
        return !(left == right);
    }
}
