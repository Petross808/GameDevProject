using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameInput : MonoBehaviour
{
    private IController _controller;

    void Awake()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions["WASD"].performed += DirStart;
        input.actions["WASD"].canceled += DirStop;
        input.actions["Space"].performed += SpacePressed;
        input.actions["MouseAim"].performed += AimChanged;
        input.actions["LMB"].performed += LMBPressed;
        input.actions["RMB"].performed += RMBPressed;
    }

    private void Start()
    {
        _controller = GetComponent<GameState>().CurrentController;
    }

    private void DirStart(InputAction.CallbackContext context)
    {
        _controller.DirStart(context);
    }

    private void DirStop(InputAction.CallbackContext context)
    {
        _controller.DirStop(context);
    }

    private void SpacePressed(InputAction.CallbackContext context)
    {
        _controller.SpacePressed(context);
    }

    private void AimChanged(InputAction.CallbackContext context)
    {
        _controller.AimChanged(context);
    }

    private void LMBPressed(InputAction.CallbackContext context)
    {
        _controller.LMBPressed(context);
    }

    private void RMBPressed(InputAction.CallbackContext context)
    {
        _controller.RMBPressed(context);
    }
}
