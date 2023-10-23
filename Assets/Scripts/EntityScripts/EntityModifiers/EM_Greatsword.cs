using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Greatsword : MonoBehaviour, IEntityModifier
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
        _attack = Resources.Load<Transform>("Greatsword");
    }

    // Register the attack in the EntityCombat to a Primary slot and inject Damage multiplied by _amount and Cooldown multiplied by 0.7 to the power of _amount
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.PRIMARY, _attack);
            _attackData.Damage *= _amount;
            _attackData.Cooldown *= Mathf.Pow(0.7f, _amount - 1);
        }
    }
}