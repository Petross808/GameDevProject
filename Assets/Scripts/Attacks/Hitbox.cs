using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttack))]
public class Hitbox : MonoBehaviour
{
    private IAttack _attack; // Attack script associated with this hitbox

    private void Awake()
    {
        _attack = GetComponent<IAttack>();
    }

    // If the layer of the colliding object is in the hitmask and the object has EntityHealth, if the attack is healing, heal the object, otherwise damage it
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer | _attack.HitMask) == _attack.HitMask)
        {
            if (collision.TryGetComponent<EntityHealth>(out EntityHealth entityHealth))
            {
                if(_attack.IsHealing)
                {
                    entityHealth.Heal(_attack.Damage);
                }
                else
                {
                    entityHealth.ReceiveDamage(_attack.Damage, this);
                }
            }
        }
    }
}
