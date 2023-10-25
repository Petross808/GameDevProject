using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EM_Hailstorm : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;

    // Increase damage of the aura by it's base damage
    public void AddAnother()
    {
        _amount++;
    }

    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("Hailstorm");
        EntityHealth.OnAnyEntityHit += FreezeEnemy;
    }

    // Register the attack in the EntityCombat to an Aura slot
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.AURA, _attack);
        }
    }

    // If enemy takes damage by Hailstorm, freeze them
    private void FreezeEnemy(object sender, HitData e)
    {
        if (e.DamageSource.gameObject.name == "Hailstorm(Clone)")
        {
            StartCoroutine(Freeze(e.DamageReceiver.gameObject));
        }
    }

    // Lower enemy's movement speed by 20% for each hailstorm owned and change their color to cyan
    private IEnumerator Freeze(GameObject enemy)
    {
        EntityMovement movement = enemy.GetComponent<EntityMovement>();
        SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
        float originalSpeed = movement.Speed;
        movement.Speed *= Mathf.Pow(0.5f,_amount);
        sprite.color = Color.cyan;

        yield return new WaitForSeconds(0.49f);

        if(enemy != null)
        {
            movement.Speed = originalSpeed;
            sprite.color = Color.white;
        }

    }
}