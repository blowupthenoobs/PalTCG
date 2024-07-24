using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Card/PalCard")]
public class PalCardData : CardData
{
    public int cost;
    public Resources.Element element;
    public List<Resources.Traits> traits = new List<Resources.Traits>();
    public int size;
    public UnityEvent<GameObject> OnDestroy;
    public UnityEvent<GameObject> OnPlay;
    public UnityEvent<GameObject> PalSkill;
}
