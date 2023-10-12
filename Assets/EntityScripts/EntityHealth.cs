using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public int _maxHealth;

    private int _health;
    public int Health { get => _health; }
    public int MaxHealth { get => _maxHealth; }

    private void Start()
    {
        _health = _maxHealth;
    }

    public virtual void ReceiveDamage(int amount, Hitbox source)
    {
        HitData hitData = new HitData(amount, source, this);
        RaiseOnEntityHit(hitData);
        _health -= hitData.DamageDealt;
        if (_health <= 0 )
        {
            Die(hitData);
        }
    }

    public virtual void Die(HitData lastHit)
    {
        RaiseOnEntityDeath(lastHit);
        Destroy(this.gameObject);
    }

    public event EventHandler<HitData> OnEntityHit;
    public static event EventHandler<HitData> OnAnyEntityHit;
    protected virtual void RaiseOnEntityHit(HitData data)
    {
        EventHandler<HitData> handler = OnEntityHit;
        if(handler != null )
        {
            handler(this, data);
        }
        handler = OnAnyEntityHit;
        if (handler != null)
        {
            handler(this, data);
        }

    }

    public event EventHandler<HitData> OnEntityDeath;
    public static event EventHandler<HitData> OnAnyEntityDeath;
    protected virtual void RaiseOnEntityDeath(HitData data)
    {
        EventHandler<HitData> handler = OnEntityDeath;
        if (handler != null)
        {
            handler(this, data);
        }
        handler = OnAnyEntityDeath;
        if (handler != null)
        {
            handler(this, data);
        }
    }
}
