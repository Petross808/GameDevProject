using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_ShipHeal : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private EntityHealth _ship;

    // Heal the ship 1/4 of it's max health
    public void AddAnother()
    {
        _amount++;
        if(_ship != null)
        {
            _ship.Heal(_ship.MaxHealth / 4);
        }
    }

    //Try to get the ship, heal it 1/4 of it's health 
    void Start()
    {
        GameObject shipGameObject = GameObject.FindGameObjectWithTag("Ship");
        if(shipGameObject != null )
        {
            if(shipGameObject.TryGetComponent<EntityHealth>(out _ship))
            {
                _ship.Heal(_ship.MaxHealth / 4);
            }
        }
    }
}
