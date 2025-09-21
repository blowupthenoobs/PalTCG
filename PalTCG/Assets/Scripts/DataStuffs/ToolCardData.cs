using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Resources;
using DefaultUnitData;
public class ToolCardData : CardData
{
    public ToolData originalData;
    public string toolType;
    public resources cost;
    public int size;

    public void DecomposeData(ToolData newData)
    {
        originalData = newData;

        cardArt = originalData.cardArt;
        toolType = originalData.toolType;
        traits = originalData.traits;
        cost = originalData.cost;
        size = originalData.size;
        currentAtk = originalData.attackPower;
        maxHp = originalData.maxHp;
        cardID = originalData.cardID;
    }

    public override bool Equals(object obj)
    {
        if(obj is ToolCardData otherData)
        {
            return originalData == otherData.originalData;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return originalData.GetHashCode();
    }

    public static bool operator ==(ToolCardData left, ToolCardData right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ToolCardData left, ToolCardData right)
    {
        return !(left == right);
    }
}
