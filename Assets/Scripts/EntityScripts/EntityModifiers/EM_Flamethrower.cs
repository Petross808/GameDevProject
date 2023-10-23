using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Flamethrower : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;

    public void AddAnother()
    {
        _amount++;
        Start();
    }

    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("Flamethrower");
    }

    // Register the attack in the EntityCombat to a Primary slot and inject Damage multiplied by _amount squared
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.PRIMARY, _attack);
            _attackData.Damage *= _amount * _amount;
        }
    }
}