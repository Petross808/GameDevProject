using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class EM_Fireball : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    [SerializeField]
    private Transform _attack;

    public void AddAnother()
    {
        _amount++;
        Start();
    }

    void Awake()
    {
        _attack = Resources.Load<Transform>("Fireball");
    }
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            ec.RegisterAttack(EntityCombat.AttackSlot.SECONDARY, _attack);
        }
    }
}
