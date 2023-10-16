using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EM_FieryPresence : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    [SerializeField]
    private Transform _attack;

    private IAttack _attackData;

    public void AddAnother()
    {
        _attackData.Damage = Mathf.CeilToInt(_attackData.Damage * (_amount + 1f) / _amount);
        _amount++;
    }

    void Start()
    {
        _attack = Resources.Load<Transform>("FieryPresence");
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.AURA, _attack);
        }
    }
}