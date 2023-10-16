using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;

    private int _health;
    public int Health { get => _health; }
    public int MaxHealth { get => _maxHealth; }

    private void Awake()
    {
        _health = _maxHealth;
    }

    public virtual void ReceiveDamage(int amount, Hitbox source)
    {
        HitData hitData = new HitData(amount, source, this);
        if (source != null)
        {
            this.RaiseEvent<HitData>(OnEntityHit, hitData);
            this.RaiseEvent<HitData>(OnAnyEntityHit, hitData);
        }

        _health -= hitData.DamageDealt;
        this.RaiseEvent(OnAfterEntityHit);
        this.RaiseEvent(OnAfterAnyEntityHit);
        if (_health <= 0)
        {
            Die(hitData);
        }

    }

    public virtual void Heal(int amount)
    {
        _health = (_health + amount) > _maxHealth ? _maxHealth : _health + amount;
        this.RaiseEvent<int>(OnAfterEntityHeal, amount);
        this.RaiseEvent<int>(OnAfterAnyEntityHeal, amount);
    }

    public virtual void SetMaxHealth(int value)
    {
        _maxHealth = (value > 0) ? value : 1;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public virtual void Die(HitData lastHit)
    {
        if(lastHit.DamageSource != null)
        {
            this.RaiseEvent<HitData>(OnEntityDeath, lastHit);
            this.RaiseEvent<HitData>(OnAnyEntityDeath, lastHit);
        }
        Destroy(this.gameObject);
    }

    public event EventHandler<HitData> OnEntityHit;
    public static event EventHandler<HitData> OnAnyEntityHit;

    public event EventHandler OnAfterEntityHit;
    public static event EventHandler OnAfterAnyEntityHit;

    public event EventHandler<HitData> OnEntityDeath;
    public static event EventHandler<HitData> OnAnyEntityDeath;

    public event EventHandler<int> OnAfterEntityHeal;
    public static event EventHandler<int> OnAfterAnyEntityHeal;

}
