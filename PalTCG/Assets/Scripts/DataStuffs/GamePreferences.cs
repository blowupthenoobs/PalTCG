using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GamePreferences : ScriptableObject
{
    public float barTransTime;

    public float maxIndividualSpacing;
    public float maxTotalSpace;
    public float cardMoveSpeed;
    public string tempDeck;
}
