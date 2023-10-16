using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IController
{
    void DirStart(InputAction.CallbackContext context);
    void DirStop(InputAction.CallbackContext context);
    void SpaceStart(InputAction.CallbackContext context);
    void SpaceStop(InputAction.CallbackContext context);
    void AimChanged(InputAction.CallbackContext context);
    void LMBStart(InputAction.CallbackContext context);
    void LMBStop(InputAction.CallbackContext context);
    void RMBStart(InputAction.CallbackContext context);
    void RMBStop(InputAction.CallbackContext context);
}
