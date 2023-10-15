using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityModifier
{
    int Amount { get; }

    void AddAnother();
}
