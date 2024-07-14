using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Card/PalCard")]
public class PalCardData : CardData
{
    public List<GameObject> cost = new List<GameObject>();
    [SerializeField] Resources.Element element;
    [SerializeField] List<Resources.Traits> traits = new List<Resources.Traits>();
    public UnityEvent<GameObject> OnDestroy;
    public UnityEvent<GameObject> OnPlay;
    public UnityEvent<GameObject> PalSkill;
}
