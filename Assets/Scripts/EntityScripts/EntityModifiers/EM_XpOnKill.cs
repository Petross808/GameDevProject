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

    void Awake()
    {
        _leveling = GetComponent<EntityLeveling>();
        EntityHealth.OnAnyEntityDeath += AddExperience;
    }

    private void AddExperience(object sender, HitData e)
    {
        if(e.DamageSource.transform.root == transform.root)
        {
            _leveling.GainExperience(200);
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityDeath -= AddExperience;
    }

    public void AddAnother()
    {
        // pass
    }
}
