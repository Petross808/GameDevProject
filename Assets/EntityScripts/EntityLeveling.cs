using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLeveling : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    private int _xpToLvlUp = 1;

    [SerializeField]
    [Min(0)]
    private float _xpRequirementMultiplier = 0;

    [SerializeField]
    [Min(1)]
    private int _maxXpRequirement = 1;

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
        this.RaiseEvent<XPData>(OnEntityGainXP, xpData);
        _experience += xpData.XpAmount;


        CheckForLevelUp();
    }

    public void LevelUp()
    {
        _level++;
        _experience -= _xpToLvlUp;
        _xpToLvlUp = Math.Min(Mathf.FloorToInt((_xpToLvlUp * _xpRequirementMultiplier) / 100) * 100, _maxXpRequirement);
        this.RaiseEvent<int>(OnEntityLevelUp, _level);
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
    public event EventHandler<int> OnEntityLevelUp;
}
