using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Controller interface
public interface IController
{
    void DirStart(InputAction.CallbackContext context); // Direction button down
    void DirStop(InputAction.CallbackContext context); // Direction button up
    void SpaceStart(InputAction.CallbackContext context); // Spacebar down
    void SpaceStop(InputAction.CallbackContext context); // Spacebar up
    void AimChanged(InputAction.CallbackContext context); // Mouse pointer moved
    void LMBStart(InputAction.CallbackContext context); // Left mouse button down
    void LMBStop(InputAction.CallbackContext context); // Left mouse button up
    void RMBStart(InputAction.CallbackContext context); // Right mouse button down
    void RMBStop(InputAction.CallbackContext context); // Right mouse button up
    void OnSwitch();
}
