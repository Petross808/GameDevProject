using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_CrystalProtection : MonoBehaviour, IEntityModifier
{

    private int _amount = 1;
    public int Amount => _amount;

    public void AddAnother()
    {
        _amount++;
    }

    void Start()
    {
        EntityHealth.OnAnyEntityHit += ReduceCrystalDamage;
    }

    private void ReduceCrystalDamage(object sender, HitData e)
    {
        if(e.DamageReceiver.gameObject.CompareTag("Crystal"))
        {
            e.DamageDealt = Mathf.CeilToInt(e.DamageDealt * Mathf.Pow(0.9f, _amount));
        }
    }

    void OnDestroy()
    {
        
    }
}
