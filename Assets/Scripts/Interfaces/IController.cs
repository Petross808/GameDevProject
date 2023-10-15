using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IController
{
    void DirStart(InputAction.CallbackContext context);
    void DirStop(InputAction.CallbackContext context);
    void SpacePressed(InputAction.CallbackContext context);
    void AimChanged(InputAction.CallbackContext context);
    void LMBPressed(InputAction.CallbackContext context);
    void RMBPressed(InputAction.CallbackContext context);
}
