using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_ShipProtection : MonoBehaviour, IEntityModifier
{

    private int _amount = 1;
    public int Amount => _amount;

    public void AddAnother()
    {
        _amount++;
    }

    void Start()
    {
        EntityHealth.OnAnyEntityHit += ReduceShipDamage;
    }

    // If the ship gets hit, multiply the damage received by 0.9 to the power of _amount
    private void ReduceShipDamage(object sender, HitData e)
    {
        if(e.DamageReceiver.gameObject.CompareTag("Ship"))
        {
            e.DamageDealt = Mathf.CeilToInt(e.DamageDealt * Mathf.Pow(0.9f, _amount));
        }
    }

    void OnDestroy()
    {
        EntityHealth.OnAnyEntityHit -= ReduceShipDamage;
    }
}
