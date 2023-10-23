using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_ShipAddMaxHealth : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private EntityHealth _ship;

    private int _healthToAdd = 200;

    // Increase ship's max health by _healthToAdd and heal the same amount
    public void AddAnother()
    {
        _amount++;
        if (_ship != null)
        {
            _ship.SetMaxHealth(_ship.MaxHealth + _healthToAdd);
            _ship.Heal(_healthToAdd);
        }
    }

    // Try to get the ship, increase it's max health by _healthToAdd and heal the same amount
    void Start()
    {
        GameObject shipGameObject = GameObject.FindGameObjectWithTag("Ship");
        if (shipGameObject != null)
        {
            if (shipGameObject.TryGetComponent<EntityHealth>(out _ship))
            {
                _ship.SetMaxHealth(_ship.MaxHealth + _healthToAdd);
                _ship.Heal(_healthToAdd);
            }
        }
    }
}
