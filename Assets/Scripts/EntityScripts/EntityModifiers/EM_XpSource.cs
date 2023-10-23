using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_XpSource : MonoBehaviour, IEntityModifier
{
    public int Amount => 1;

    [SerializeField]
    private int xpValue = 0; // When an entity with xpOnKill kills this entity, it gains the xpValue

    public int XpValue => xpValue;

    public void AddAnother() { }

}