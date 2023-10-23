using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EM_FieryPresence : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;

    // Increase damage of the aura by it's base damage
    public void AddAnother()
    {
        _attackData.Damage = Mathf.CeilToInt(_attackData.Damage * (_amount + 1f) / _amount);
        _amount++;
    }

    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("FieryPresence");
    }

    // Register the attack in the EntityCombat to an Aura slot
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.AURA, _attack);
        }
    }
}