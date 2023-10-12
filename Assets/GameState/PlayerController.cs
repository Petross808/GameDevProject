using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EntityCombat))]
public class PlayerController : MonoBehaviour, IController
{
    [SerializeField]
    private Transform _aim;

    private EntityCombat _attackManager;
    private EntityMovement _movement;
    private Vector2 _mousePosition;

    void Awake()
    {
        _attackManager = GetComponent<EntityCombat>();
        _movement = GetComponent<EntityMovement>();
    }

    public void DirStart(InputAction.CallbackContext context)
    {
        _movement.MoveStart(context.ReadValue<Vector2>());
    }

    public void DirStop(InputAction.CallbackContext context)
    {
        _movement.MoveEnd();
    }
    public void SpacePressed(InputAction.CallbackContext context)
    {
        
    }
    public void AimChanged(InputAction.CallbackContext context)
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void LMBPressed(InputAction.CallbackContext context)
    {
        _attackManager.PrimaryAttack();
    }

    public void RMBPressed(InputAction.CallbackContext context)
    {
        _attackManager.SecondaryAttack();
    }

    private void Update()
    {
        _aim.transform.up = _mousePosition - (Vector2)transform.position;
    }
}
