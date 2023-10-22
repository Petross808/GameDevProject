using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [SerializeField]
    private Transform _baseAttackTemplate;
    [SerializeField]
    private Transform _aim;
    [SerializeField]
    private LayerMask _attackHitMask;

    private IAttack[] _attacks;
    private List<IAttack> _auras;

    public LayerMask AttackHitMask { get => _attackHitMask; }

    public enum AttackSlot
    {
        PRIMARY = 0,
        SECONDARY = 1,
        UTILITY = 2,
        AURA = 3
    }

    void Awake()
    {
        _attacks = new IAttack[3];
        _auras = new List<IAttack>();
        RegisterAttack(AttackSlot.PRIMARY, _baseAttackTemplate);
    }

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
