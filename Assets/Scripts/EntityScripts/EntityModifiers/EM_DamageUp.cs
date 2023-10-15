using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_DamageUp : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    public void AddAnother()
    {
        _amount++;
    }

    void Start()
    {
        EntityHealth.OnAnyEntityHit += AddDamageMultiplier;
    }

    private void AddDamageMultiplier(object sender, HitData e)
    {
        if(e.DamageSource.transform.root == transform.root)
        {
            e.DamageDealt = Mathf.FloorToInt(e.DamageDealt * (1 + (0.2f * _amount)));
        }
    }


    void OnDestroy() 
    {
        EntityHealth.OnAnyEntityHit -= AddDamageMultiplier;
    }
}
