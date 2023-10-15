using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_SpeedUp : MonoBehaviour, IEntityModifier
{
    private int _amount;
    public int Amount => _amount;

    public void AddAnother()
    {
        _amount++;
        Start();
    }

    void Start()
    {
        if(TryGetComponent<EntityMovement>(out EntityMovement entityMovement))
        {
            entityMovement.Speed += 2f;
        }
    }

}
