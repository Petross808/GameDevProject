using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLeveling : MonoBehaviour
{
    [SerializeField]
    private int _xpToLvlUp;
    [SerializeField]
    private float _xpRequirementMultiplier;
    [SerializeField]
    private int _maxXpRequirement;

    private int _experience;
    private int _level;

    public int Experience { get => _experience; }
    public int Level { get => _level; }
    public int XpToLvlUp { get => _xpToLvlUp; }
    public float XpRequirementMultiplier { get => _xpRequirementMultiplier; }

    void Awake()
    {
        _experience = 0;
        _level = 1;
    }

    public void GainExperience(int amount)
    {
        XPData xpData = new XPData(amount);
        RaiseOnEntityGainXP(xpData);
        _experience += xpData.XpAmount;


        CheckForLevelUp();
    }

    public void LevelUp()
    {
        _level++;
        _xpToLvlUp = Math.Min(Mathf.FloorToInt((_xpToLvlUp * _xpRequirementMultiplier) / 100) * 100, _maxXpRequirement);
        RaiseOnEntityLevelUp(_level);
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (_experience >= _xpToLvlUp)
        {
            LevelUp();
        }
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
