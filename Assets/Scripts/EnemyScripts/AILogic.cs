using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EntityCombat))]
public class AILogic : MonoBehaviour
{
    [SerializeField]
    private float _range;
    [SerializeField]
    private Transform _aim;
    [SerializeField]
    private GameObject _target;

    private EntityMovement _movement;
    private EntityCombat _attackManager;

    public GameObject Target { get => _target; set => _target = value; }

    private void Awake()
    {
        _movement = GetComponent<EntityMovement>();
        _attackManager = GetComponent<EntityCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_target == null)
        {
            _movement.MoveEnd();
            return;
        }

        if (Vector2.Distance(transform.position, _target.transform.position) < _range)
        {
            AttackTarget();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        _movement.MoveStart(Vector3.Normalize(_target.transform.position - transform.position));
    }

    void AttackTarget()
    {
        _movement.MoveEnd();
        _aim.transform.up = (Vector2)_target.transform.position - (Vector2)transform.position;
        _attackManager.PrimaryAttack();
    }
}
