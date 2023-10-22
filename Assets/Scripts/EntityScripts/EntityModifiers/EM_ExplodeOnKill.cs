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

    void Start()
    {
        _explosion = Resources.Load<Transform>("Explosion");

        if (TryGetComponent<EntityCombat>(out _combat))
        {
            _hitMask = _combat.AttackHitMask;
            EntityHealth.OnAnyEntityDeath += SpawnExplosion;
        }
    }

    private void SpawnExplosion(object sender, HitData e)
    {
        if(e.DamageSource.transform.root == transform.root)
        {
            Transform instance = Instantiate(_explosion, e.DamageReceiver.transform.position, e.DamageReceiver.transform.rotation);
            instance.GetComponent<IAttack>().HitMask = _hitMask;
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityDeath -= SpawnExplosion;
    }
}