using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EM_HealingAura : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;

    public void AddAnother()
    {
        _amount++;
    }
    
    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("HealingAura");
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