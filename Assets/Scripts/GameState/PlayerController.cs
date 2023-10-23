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
    private Transform _aim; // Aim child object

    private EntityCombat _attackManager;
    private EntityMovement _movement;
    private Vector2 _mousePosition; // Position of the mouse pointer on the screen

    private bool _lmbHeld; // Is left mouse button down
    private bool _rmbHeld; // Is right mouse button down
    private bool _spaceHeld; // Is spacebar down

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

    public void AimChanged(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void LMBStart(InputAction.CallbackContext context)
    {
        _lmbHeld = true;
    }

    public void LMBStop(InputAction.CallbackContext context)
    {
        _lmbHeld = false;
    }

    public void RMBStart(InputAction.CallbackContext context)
    {
        _rmbHeld = true;
    }

    public void RMBStop(InputAction.CallbackContext context)
    {
        _rmbHeld = false;
    }

    public void SpaceStart(InputAction.CallbackContext context)
    {
        _spaceHeld = true;
    }

    public void SpaceStop(InputAction.CallbackContext context)
    {
        _spaceHeld = false;
    }

    // Reset state on switch
    public void OnSwitch()
    {
        _movement.MoveEnd();
        _lmbHeld = false;
        _spaceHeld = false;
        _rmbHeld= false;
    }

    // Rotate aim towards the mouse pointer every frame and try to use attacks when the corresponding button is held
    private void Update()
    {
        _aim.transform.up = (Vector2)Camera.main.ScreenToWorldPoint(_mousePosition) - (Vector2)transform.position;

        if(_lmbHeld) _attackManager.UseAttack(EntityCombat.AttackSlot.PRIMARY);
        if(_rmbHeld) _attackManager.UseAttack(EntityCombat.AttackSlot.SECONDARY);
        if(_spaceHeld) _attackManager.UseAttack(EntityCombat.AttackSlot.UTILITY);
    }
}
