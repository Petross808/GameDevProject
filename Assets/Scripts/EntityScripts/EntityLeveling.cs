using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLeveling : MonoBehaviour
{
    [SerializeField]
    [Min(1)]
    private int _xpToLvlUp = 1; // How much xp is needed for a level up

    [SerializeField]
    [Min(0)]
    private float _xpRequirementMultiplier = 0; // How much does the xp requirement increase each level

    [SerializeField]
    [Min(1)]
    private int _maxXpRequirement = 1; // Maximum limit for the xp requirement

    private int _experience = 0;
    private int _level = 1;

    public int Experience { get => _experience; }
    public int Level { get => _level; }
    public int XpToLvlUp { get => _xpToLvlUp; }
    public float XpRequirementMultiplier { get => _xpRequirementMultiplier; }


    // Raise OnEntityGainXP events to allow subscribers to modify the amount, then add the amount to _experience, then check for level up
    public void GainExperience(int amount)
    {
        XPData xpData = new XPData(amount);
        this.RaiseEvent<XPData>(OnEntityGainXP, xpData);
        this.RaiseEvent<XPData>(OnAnyEntityGainXP, xpData);
        _experience += xpData.XpAmount;
        CheckForLevelUp();
    }

    // Add a level, lower _experince by the amount that was needed and calculate the next xp requirement, then raise OnEntityLevelUp events, then check for level up
    public void LevelUp()
    {
        _level++;
        _experience -= _xpToLvlUp;
        _xpToLvlUp = Math.Min(Mathf.FloorToInt((_xpToLvlUp * _xpRequirementMultiplier) / 100) * 100, _maxXpRequirement);
        this.RaiseEvent<int>(OnEntityLevelUp, _level);
        this.RaiseEvent<int>(OnAnyEntityLevelUp, _level);
        CheckForLevelUp();
    }

    // Raise OnAfterEntityGainXP events, then if there's enough experience for level up, level up
    private void CheckForLevelUp()
    {
        this.RaiseEvent(OnAfterEntityGainXP);
        this.RaiseEvent(OnAfterAnyEntityGainXP);

        if (_experience >= _xpToLvlUp)
        {
            LevelUp();
        }
    }

    public event EventHandler<XPData> OnEntityGainXP;
    public event EventHandler OnAfterEntityGainXP;
    public event EventHandler<int> OnEntityLevelUp;
    public static event EventHandler<XPData> OnAnyEntityGainXP;
    public static event EventHandler OnAfterAnyEntityGainXP;
    public static event EventHandler<int> OnAnyEntityLevelUp;

}
