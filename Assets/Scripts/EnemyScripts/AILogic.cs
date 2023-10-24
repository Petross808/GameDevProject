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
    private float _range; // Enemy attack range
    [SerializeField]
    private Transform _aim; // Enemy aim child object
    [SerializeField]
    private GameObject _target; // Target for the enemy to follow

    private EntityMovement _movement; // Movement component
    private EntityCombat _combat; // Combat component

    public GameObject Target { get => _target; set => _target = value; } // Exposing Target property to allow EnemySpawners to inject a target at runtime

    private void Awake()
    {
        _movement = GetComponent<EntityMovement>();
        _combat = GetComponent<EntityCombat>();
    }

    void Update()
    {
        // if target is not defined, stand still
        if (_target == null)
        {
            _movement.MoveEnd();
            return;
        }

        // if target is in range, attack it, else move towards it
        if (Vector2.Distance(_aim.transform.position, _target.transform.position) < _range)
        {
            AttackTarget();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    // Move in the direction of the target
    void MoveTowardsTarget()
    {
        _movement.MoveStart(Vector3.Normalize(_target.transform.position - transform.position));
    }

    // Stand still, aim at the target and try to attack
    void AttackTarget()
    {
        _movement.MoveEnd();
        _aim.transform.up = (Vector2)_target.transform.position - (Vector2)transform.position;
        _combat.UseAttack(EntityCombat.AttackSlot.PRIMARY);
    }
}
