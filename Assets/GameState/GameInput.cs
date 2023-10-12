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
        _playerInput.actions["Space"].performed += SpacePressed;
        _playerInput.actions["MouseAim"].performed += AimChanged;
        _playerInput.actions["LMB"].performed += LMBPressed;
        _playerInput.actions["RMB"].performed += RMBPressed;
    }

    private void DirStart(InputAction.CallbackContext context)
    {
        _controller?.DirStart(context);
    }

    private void DirStop(InputAction.CallbackContext context)
    {
        _controller?.DirStop(context);
    }

    private void SpacePressed(InputAction.CallbackContext context)
    {
        _controller?.SpacePressed(context);
    }

    private void AimChanged(InputAction.CallbackContext context)
    {
        _controller?.AimChanged(context);
    }

    private void LMBPressed(InputAction.CallbackContext context)
    {
        _controller?.LMBPressed(context);
    }

    private void RMBPressed(InputAction.CallbackContext context)
    {
        _controller?.RMBPressed(context);
    }

    private void OnDestroy()
    {
        _playerInput.actions["WASD"].performed -= DirStart;
        _playerInput.actions["WASD"].canceled -= DirStop;
        _playerInput.actions["Space"].performed -= SpacePressed;
        _playerInput.actions["MouseAim"].performed -= AimChanged;
        _playerInput.actions["LMB"].performed -= LMBPressed;
        _playerInput.actions["RMB"].performed -= RMBPressed;
    }
}
