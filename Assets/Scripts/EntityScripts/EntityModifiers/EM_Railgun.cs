using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Railgun : MonoBehaviour, IEntityModifier
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
        _attack = Resources.Load<Transform>("Railgun");
    }

    // Register the attack in the EntityCombat to a Secondary slot and inject Damage multiplied by _amount and Cooldown multiplied by 0.9 to the power of (_amount - 1)
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.SECONDARY, _attack);
            _attackData.Damage *= _amount;
            _attackData.Cooldown *= Mathf.Pow(0.9f, _amount - 1);
        }
    }
}
