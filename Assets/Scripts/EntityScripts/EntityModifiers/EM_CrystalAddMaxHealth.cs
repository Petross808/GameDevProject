using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_CrystalAddMaxHealth : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private EntityHealth _crystal;

    private int _healthToAdd = 200;
    public void AddAnother()
    {
        _amount++;
        if (_crystal != null)
        {
            _crystal.SetMaxHealth(_crystal.MaxHealth + _healthToAdd);
            _crystal.Heal(_healthToAdd);
        }
    }

    void Start()
    {
        GameObject crystalGO = GameObject.FindGameObjectWithTag("Crystal");
        if (crystalGO != null)
        {
            if (crystalGO.TryGetComponent<EntityHealth>(out _crystal))
            {
                _crystal.SetMaxHealth(_crystal.MaxHealth + _healthToAdd);
                _crystal.Heal(_healthToAdd);
            }
        }
    }
}
