using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [SerializeField]
    private Transform _baseAttackTemplate; // Default Primary attack
    [SerializeField]
    private Transform _aim; // Entity aim child object
    [SerializeField]
    private LayerMask _attackHitMask; // What should attacks collide with

    private IAttack[] _attacks; // Main three attack slots (Primary, Secondary, Utility)
    private List<IAttack> _auras; // Aura slots

    public LayerMask AttackHitMask { get => _attackHitMask; }

    public enum AttackSlot
    {
        PRIMARY = 0,
        SECONDARY = 1,
        UTILITY = 2,
        AURA = 3
    }

    // Initialize variables and register default primary attack
    void Awake()
    {
        _attacks = new IAttack[3];
        _auras = new List<IAttack>();
        RegisterAttack(AttackSlot.PRIMARY, _baseAttackTemplate);
    }

    // Instantiate provided attack prefab as a child of this gameObject, add it's IAttack component to the appropriate list and inject a hitmask into it, then return the IAttack component
    public IAttack RegisterAttack(AttackSlot slot, Transform attackToRegister)
    {
        Transform attackTransform = Instantiate(attackToRegister, transform, false);
        IAttack attack = attackTransform.GetComponent<IAttack>();
        if(slot != AttackSlot.AURA)
        {
            _attacks[(int)slot] = attack;
            attackTransform.gameObject.SetActive(false);
            _attacks[(int)slot].HitMask = _attackHitMask;
        }
        else
        {
            _auras.Add(attack);
            _auras.Last().HitMask = _attackHitMask;
        }

        return attack;
    }

    // Call the Attack function of the attack in the provided attack slot, if attack was successful, raise OnEntityAttack events
    public void UseAttack(AttackSlot slot)
    {
        if(_attacks[(int)slot] != null)
        {
            if (_attacks[(int)slot].Attack(_aim))
            {
                AttackData data = new(this, _attacks[(int)slot], slot);
                this.RaiseEvent<AttackData>(OnEntityAttack, data);
                this.RaiseEvent<AttackData>(OnAnyEntityAttack, data);
            }
        }
    }

    // Returns attack at slot
    public IAttack GetAttack(AttackSlot slot)
    {
        return _attacks[(int)slot];
    }

    // Tick down cooldowns of all attacks in the attack slots (Update doesn't run in disabled objects)
    void Update()
    {
        foreach (var attack in _attacks)
        {
            attack?.CooldownTick();
        }
    }

    public event EventHandler<AttackData> OnEntityAttack;
    public static event EventHandler<AttackData> OnAnyEntityAttack;
}
