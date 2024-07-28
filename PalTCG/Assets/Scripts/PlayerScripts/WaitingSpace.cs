using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingSpace : MonoBehaviour
{
    [SerializeField] GameObject waiting3;
    [SerializeField] GameObject waiting2;
    [SerializeField] GameObject waiting1;
    [SerializeField] GameObject readyspot;

    List<GameObject> readyCards = new List<GameObject>();
    List<GameObject> TurnsTillReady1 = new List<GameObject>();
    List<GameObject> TurnsTillReady2 = new List<GameObject>();
    List<GameObject> TurnsTillReady3 = new List<GameObject>();


    [SerializeField] GameObject cardPrefab;

    void Start() //Do this first to garuentee that GameManager has a value as start happens after awake
    {
        GameManager.Instance.StartPlayerTurn += MoveWaitlist;
    }

    public void AddToWaitlist(PalCardData data)
    {
        int size = data.size;

        if(size == 0)
        {
            
        }
    }

    void MoveWaitlist()
    {
        while(TurnsTillReady1.Count > 0)
        {
            readyCards.Add(TurnsTillReady1[0]);
            TurnsTillReady1.RemoveAt(0);
        }

        while(TurnsTillReady2.Count > 0)
        {
            TurnsTillReady1.Add(TurnsTillReady2[0]);
            TurnsTillReady2.RemoveAt(0);
        }

        while(TurnsTillReady3.Count > 0)
        {
            TurnsTillReady2.Add(TurnsTillReady3[0]);
            TurnsTillReady3.RemoveAt(0);
        }
    }
}
