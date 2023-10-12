using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPData
{
    private int _xpAmount;
    
    public XPData(int xpAmount)
    {
        _xpAmount = xpAmount;
    }

    public int XpAmount { get => _xpAmount; set => _xpAmount = value; }
}
