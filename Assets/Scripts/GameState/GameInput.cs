using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(PlayerInput))]
public class GameInput : MonoBehaviour
{
    private IController _controller;
    private PlayerInput _playerInput;

    public IController Controller { get => _controller; set => _controller = value; }

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["WASD"].performed += DirStart;
        _playerInput.actions["WASD"].canceled += DirStop;
        _playerInput.actions["LMB"].performed += LMBStart;
        _playerInput.actions["LMB"].canceled += LMBStop;
        _playerInput.actions["RMB"].performed += RMBStart;
        _playerInput.actions["RMB"].canceled += RMBStop;
        _playerInput.actions["Space"].performed += SpaceStart;
        _playerInput.actions["Space"].canceled += SpaceStop;
        _playerInput.actions["MouseAim"].performed += AimChanged;
    }

    private void DirStart(InputAction.CallbackContext context)
    {
        _controller?.DirStart(context);
    }

    private void DirStop(InputAction.CallbackContext context)
    {
        _controller?.DirStop(context);
    }

    private void SpaceStart(InputAction.CallbackContext context)
    {
        _controller?.SpaceStart(context);
    }

    private void SpaceStop(InputAction.CallbackContext context)
    {
        _controller?.SpaceStop(context);
    }

    private void AimChanged(InputAction.CallbackContext context)
    {
        _controller?.AimChanged(context);
    }

    private void LMBStart(InputAction.CallbackContext context)
    {
        _controller?.LMBStart(context);
    }

    private void LMBStop(InputAction.CallbackContext context)
    {
        _controller?.LMBStop(context);
    }

    private void RMBStart(InputAction.CallbackContext context)
    {
        _controller?.RMBStart(context);
    }

    private void RMBStop(InputAction.CallbackContext context)
    {
        _controller?.RMBStop(context);
    }

    private void OnDestroy()
    {
        _playerInput.actions["WASD"].performed -= DirStart;
        _playerInput.actions["WASD"].canceled -= DirStop;
        _playerInput.actions["LMB"].performed -= LMBStart;
        _playerInput.actions["LMB"].canceled -= LMBStop;
        _playerInput.actions["RMB"].performed -= RMBStart;
        _playerInput.actions["RMB"].canceled -= RMBStop;
        _playerInput.actions["Space"].performed -= SpaceStart;
        _playerInput.actions["Space"].canceled -= SpaceStop;
        _playerInput.actions["MouseAim"].performed -= AimChanged;
    }
}
