using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PalCard : CardData
{
    public UnityEvent<GameObject> OnDestroy;
    public UnityEvent<GameObject> OnPlay;
    public UnityEvent<GameObject> ActivateAbility;
}
