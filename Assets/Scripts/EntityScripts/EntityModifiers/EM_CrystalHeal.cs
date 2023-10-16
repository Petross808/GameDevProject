using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_CrystalHeal : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private EntityHealth _crystal;

    public void AddAnother()
    {
        _amount++;
        if(_crystal != null)
        {
            _crystal.Heal(_crystal.MaxHealth / 4);
        }
    }

    void Start()
    {
        GameObject crystalGO = GameObject.FindGameObjectWithTag("Crystal");
        if(crystalGO != null )
        {
            if(crystalGO.TryGetComponent<EntityHealth>(out _crystal))
            {
                _crystal.Heal(_crystal.MaxHealth / 4);
            }
        }
    }
}
