using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Adrenaline : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;
    private EntityCombat _combat;

    public void AddAnother()
    {
        _amount++;
        Start();
    }

    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("Adrenaline");

        if(TryGetComponent<EntityCombat>(out _combat))
        {
            EntityCombat.OnAnyEntityAttack += Adrenaline;
        }
    }

    // Register the attack in the EntityCombat to a Utility slot and inject Cooldown multiplied by 0.8 to the power of (_amount - 1)
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.UTILITY, _attack);
            _attackData.Cooldown *= Mathf.Pow(0.8f, _amount - 1);
        }
    }

    private void Adrenaline(object sender, AttackData e)
    {
        if(e.EntityCombat.CompareTag("Player")
            && e.Slot == EntityCombat.AttackSlot.UTILITY)
        {
            StartCoroutine(StartAdrenaline());
        }
    }

    private IEnumerator StartAdrenaline()
    {
        IAttack primary = _combat.GetAttack(EntityCombat.AttackSlot.PRIMARY);
        float halfCooldown = primary.Cooldown / 2f;
        primary.Cooldown -= halfCooldown;
        yield return new WaitForSeconds(4f);
        primary.Cooldown += halfCooldown;

    }

}