using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data class for OnEntityGainXP events
public class XPData
{
    private int _xpAmount; // Amount of XP gained
    
    public XPData(int xpAmount)
    {
        _xpAmount = xpAmount;
    }

    public int XpAmount { get => _xpAmount; set => _xpAmount = value; }
}
