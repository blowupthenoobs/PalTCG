using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Card/PalCard")]
public class PalCard : CardData
{
    [SerializeField] Resources.Element element;
    [SerializeField] Resources.Traits traits;
    public UnityEvent<GameObject> OnDestroy;
    public UnityEvent<GameObject> OnPlay;
    public UnityEvent<GameObject> ActivateAbility;
}
