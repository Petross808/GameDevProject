using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Retaliate : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private float _percentageRetaliated = 0.25f;

    public void AddAnother()
    {
        _amount++;
    }

    void Start()
    {
        EntityHealth.OnAnyEntityHit += Retaliate;
    }

    // When the ship gets hit, deal 25% of the damage times _amount to the source
    private void Retaliate(object sender, HitData e)
    {
        if(e.DamageReceiver.gameObject.CompareTag("Ship"))
        {
            if(e.DamageSource.transform.root.TryGetComponent<EntityHealth>(out EntityHealth sourceHealth))
            {
                sourceHealth.ReceiveDamage(Mathf.CeilToInt(e.DamageDealt * _percentageRetaliated) * _amount, null);
            }
        }
    }

    void OnDestroy()
    {
        EntityHealth.OnAnyEntityHit -= Retaliate;
    }
}
