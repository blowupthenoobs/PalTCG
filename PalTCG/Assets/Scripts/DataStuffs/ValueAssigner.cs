using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


using DefaultUnitData;

public class ValueAssigner : MonoBehaviour
{
    private AccountManager manager;
    
    void Start()
    {
        manager  = AccountManager.Instance;
    }
}
