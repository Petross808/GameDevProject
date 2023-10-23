using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NullController : IController
{
    // Controller that ignores all input
    public void AimChanged(InputAction.CallbackContext context) { }
    public void DirStart(InputAction.CallbackContext context) { }
    public void DirStop(InputAction.CallbackContext context) { }
    public void LMBStart(InputAction.CallbackContext context) { }
    public void LMBStop(InputAction.CallbackContext context) { }
    public void RMBStart(InputAction.CallbackContext context) { }
    public void RMBStop(InputAction.CallbackContext context) { }
    public void SpaceStart(InputAction.CallbackContext context) { }
    public void SpaceStop(InputAction.CallbackContext context) { }
    public void OnSwitch() { }
}
