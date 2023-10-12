using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLeveling : MonoBehaviour
{
    private int _experience;
    private int _level;
    private int _xpToLvlUp;
    private float _xpRequirementMultiplier;
    private int _maxXpRequirement;

    public int Experience { get => _experience; }
    public int Level { get => _level; }
    public int XpToLvlUp { get => _xpToLvlUp; }
    public float XpRequirementMultiplier { get => _xpRequirementMultiplier; }


    // Start is called before the first frame update
    void Start()
    {
        _experience = 0;
        _level = 1;
        _xpToLvlUp = 1000;
        _xpRequirementMultiplier = 1.2f;
        _maxXpRequirement = 26000;
    }

    public void GainExperience(int amount)
    {
        XPData xpData = new XPData(amount);
        RaiseOnEntityGainXP(xpData);
        _experience += xpData.XpAmount;


        if(_experience >= _xpToLvlUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _level++;
        _xpToLvlUp = Math.Min(Mathf.FloorToInt((_xpToLvlUp * _xpRequirementMultiplier) / 100) * 100, _maxXpRequirement);
        RaiseOnEntityLevelUp(_level);

    }

    public event EventHandler<XPData> OnEntityGainXP;
    protected virtual void RaiseOnEntityGainXP(XPData xpData)
    {
        EventHandler<XPData> handler = OnEntityGainXP;
        if (handler != null)
        {
            handler(this, xpData);
        }
    }

    public event EventHandler<int> OnEntityLevelUp;
    protected virtual void RaiseOnEntityLevelUp(int currentLevel)
    {
        EventHandler<int> handler = OnEntityLevelUp;
        if (handler != null)
        {
            handler(this, currentLevel);
        }
    }


}
