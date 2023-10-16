using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EM_DoubleXP : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private EntityLeveling _leveling;
    public void AddAnother()
    {
        _amount++;
    }

    void Start()
    {
        if(TryGetComponent<EntityLeveling>(out _leveling)) 
        {
            _leveling.OnEntityGainXP += DoubleXp;
        }
    }

    private void DoubleXp(object sender, XPData e)
    {
        e.XpAmount *= (2 * _amount);
    }

    void OnDestroy() 
    {
        if(_leveling != null)
        {
            _leveling.OnEntityGainXP -= DoubleXp;
        }
    }
}