using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entity Modifier (EM) interface
public interface IEntityModifier
{
    int Amount { get; } // Amount of this modifier (instead of adding the script twice)

    void AddAnother(); // What happens when the same modifier is added again
}
