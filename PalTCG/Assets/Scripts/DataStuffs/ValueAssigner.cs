using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


using DefaultUnitData;

public class ValueAssigner : MonoBehaviour
{
    private AccountManager manager;
    private PalAbilitySets abilitySets;
    public List<Sprite> dumudSprites;
    
    void Start()
    {
        manager  = AccountManager.Instance;
        abilitySets = manager.PalAbilities;

        abilitySets.daedream.WhenAttack.Add(() => StartCoroutine(StatusEffects.PutToSleep()));
    }
}
