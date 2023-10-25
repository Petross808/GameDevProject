using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Dash : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;
    private EntityMovement _movement;

    public void AddAnother()
    {
        _amount++;
        Start();
    }

    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("Dash");

        if(TryGetComponent<EntityMovement>(out _movement))
        {
            EntityCombat.OnAnyEntityAttack += Dash;
        }
    }

    // Register the attack in the EntityCombat to a Utility slot and inject Cooldown multiplied by 0.7 to the power of (_amount - 1)
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.UTILITY, _attack);
            _attackData.Cooldown *= Mathf.Pow(0.7f, _amount - 1);
        }
    }

    private void Dash(object sender, AttackData e)
    {
        if(e.EntityCombat.CompareTag("Player")
            && e.Slot == EntityCombat.AttackSlot.UTILITY)
        {
            StartCoroutine(StartDash());
        }
    }

    private IEnumerator StartDash()
    {
        _movement.Speed += 20;
        for(int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.05f);
            _movement.Speed -= 1;
        }
    }

}