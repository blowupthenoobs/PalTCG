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

        OnDestroy = originalData.abilities.OnDestroy;
        WhenAttack = originalData.abilities.WhenAttack;
        OnAttack = originalData.abilities.OnAttack;
        OnHurt = originalData.abilities.OnHurt;
        OnPlay = originalData.abilities.OnPlay;
        EndOfTurn = originalData.abilities.EndOfTurn;
        OncePerTurn = originalData.abilities.OncePerTurn;

        PalSkill = originalData.abilities.PalSkill;
        WhenSkillAttack = originalData.abilities.WhenSkillAttack;
        OnSkillAttack = originalData.abilities.OnSkillAttack;
        EndOfSkillTurn = originalData.abilities.EndOfSkillTurn;

        foreach(UnityAction unused in OncePerTurn)
        {
            AbilityUseCounter.Add(0);
        }
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
