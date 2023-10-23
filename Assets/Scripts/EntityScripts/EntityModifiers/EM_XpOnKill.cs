using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(EntityLeveling))]
public class EM_XpOnKill : MonoBehaviour, IEntityModifier
{
    private EntityLeveling _leveling;

    public int Amount => 1;
    public void AddAnother() { }

    void Awake()
    {
        _leveling = GetComponent<EntityLeveling>();
        EntityHealth.OnAnyEntityDeath += AddExperience;
    }

    // When anything dies, if this entity killed it and it is an XpSource, gain its xp value
    private void AddExperience(object sender, HitData e)
    {
        if(e.DamageSource.transform.root == transform.root)
        {
            if(e.DamageReceiver.TryGetComponent<EM_XpSource>(out EM_XpSource xpSource))
            {
                _leveling.GainExperience(xpSource.XpValue);
            }
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityDeath -= AddExperience;
    }
}
