using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EM_XpOnKill : MonoBehaviour
{
    private EntityLeveling _leveling;

    // Start is called before the first frame update
    void Start()
    {
        _leveling = GetComponent<EntityLeveling>();
        if( _leveling != null)
        {
            EntityHealth.OnAnyEntityDeath += AddExperience;
        }
    }

    private void AddExperience(object sender, HitData e)
    {
        if(e.DamageSource.transform.root == transform.root)
        {
            _leveling.GainExperience(200);
            Debug.Log("XP");
        }
    }
}
