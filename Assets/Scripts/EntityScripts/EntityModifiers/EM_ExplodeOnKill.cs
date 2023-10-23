using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_ExplodeOnKill : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private EntityCombat _combat;
    private Transform _explosion;
    private LayerMask _hitMask;

    public void AddAnother()
    {
        _amount++;
    }

    // If this entity has EntityCombat, save it's hitmask and register OnAnyEntityDeath event
    void Start()
    {
        _explosion = Resources.Load<Transform>("Explosion");

        if (TryGetComponent<EntityCombat>(out _combat))
        {
            _hitMask = _combat.AttackHitMask;
            EntityHealth.OnAnyEntityDeath += SpawnExplosion;
        }
    }

    // When entity dies, if it was killed by this entity, spawn an explosion and inject proper Hitmask and Damage to the power of _amount into it
    private void SpawnExplosion(object sender, HitData e)
    {
        if(e.DamageSource.transform.root == transform.root)
        {
            Transform instance = Instantiate(_explosion, e.DamageReceiver.transform.position, e.DamageReceiver.transform.rotation);
            IAttack explosionAttack = instance.GetComponent<IAttack>();
            explosionAttack.HitMask = _hitMask;
            explosionAttack.Damage = (int )Mathf.Pow(explosionAttack.Damage, _amount);
            
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityDeath -= SpawnExplosion;
    }
}