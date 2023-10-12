using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private IAttack _attack;

    private void Start()
    {
        _attack = GetComponent<IAttack>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        if((1 << collision.gameObject.layer | _attack.HitMask) == _attack.HitMask)
        {
            EntityHealth entityHealth = collision.GetComponent<EntityHealth>();
            if (entityHealth != null)
            {

                entityHealth.ReceiveDamage(_attack.Damage, this);
            }

            
        }
    }
}
