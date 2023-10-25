using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_Sword : MonoBehaviour, IEntityModifier
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
        _attack = Resources.Load<Transform>("Sword");
    }

    // Register the attack in the EntityCombat to a Primary slot and inject Damage increased by (_amount - 1) * 10 and Cooldown multiplied by 0.9 to the power of (_amount - 1)
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.PRIMARY, _attack);
            _attackData.Damage += (_amount - 1) * 10;
            _attackData.Cooldown *= Mathf.Pow(0.9f, _amount - 1);
        }
    }
}