using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class AILogic : MonoBehaviour
{
    public float _range;
    public Transform _aim;

    private EntityMovement _movement;
    private EntityCombat _attackManager;
    private GameObject _crystal;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<EntityMovement>();
        _attackManager = GetComponent<EntityCombat>();
        _crystal = GameObject.FindGameObjectWithTag("Crystal");      
    }

    // Update is called once per frame
    void Update()
    {
        if(_crystal == null)
        {
            _movement.MoveEnd();
            return;
        }

        if (Vector2.Distance(transform.position, _crystal.transform.position) < _range)
        {
            AttackCrystal();
        }
        else
        {
            MoveTowardsCrystal();
        }
    }

    /*
    void MoveTowardsCrystal()
    {
        _movement.MoveStart(new(Math.Sign(_crystal.transform.position.x - transform.position.x), 0));
    }*/

    void MoveTowardsCrystal()
    {
        _movement.MoveStart(Vector3.Normalize(_crystal.transform.position - transform.position));
    }

    void AttackCrystal()
    {
        _movement.MoveEnd();
        _aim.transform.up = _crystal.transform.position - transform.position;
        _attackManager.PrimaryAttack();
    }
}
