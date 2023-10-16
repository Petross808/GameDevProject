using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttack))]
public class Hitbox : MonoBehaviour
{
    private IAttack _attack;

    private void Awake()
    {
        _attack = GetComponent<IAttack>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
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
