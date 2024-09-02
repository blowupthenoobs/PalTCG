using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Card/PalCard")]
public class PalCardData : CardData
{
    public int cost;
    public Resources.Element element;
    public Resources.Traits traits;
    public int size;
    public int maxHp;
    public int currentHp;
    public UnityEvent<GameObject> OnDestroy;
    public UnityEvent<GameObject> OnPlay;
    public UnityEvent<GameObject> PalSkill;
}
