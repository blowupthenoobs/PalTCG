using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


using DefaultUnitData;

public class ValueAssigner : MonoBehaviour
{
    private AccountManager manager;
    private PalAbilitySets abilitySets;
    
    void Start()
    {
        manager  = AccountManager.Instance;
        abilitySets = manager.PalAbilities;

        abilitySets.foxsparks.OnAttack.Add(() => StatusEffectAbilities.BurnCard());
        abilitySets.sparkit.OnAttack.Add(() => StatusEffectAbilities.ShockTarget());

        abilitySets.daedream.WhenAttack.Add(() => StartCoroutine(StatusEffectAbilities.PutToSleep()));
        abilitySets.depresso.OnAttack.Add(() => StatusEffectAbilities.PoisonCard());
        abilitySets.daedream.OnAttack.Add(() => StatusEffectAbilities.PoisonCard());
    }
}
