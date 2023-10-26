using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_FrostBolt : MonoBehaviour, IEntityModifier
{
    private int _amount = 1;
    public int Amount => _amount;

    private Transform _attack;

    private IAttack _attackData;

    public void AddAnother()
    {
        _amount++;
        Start();
    }

    // Load the attack
    void Awake()
    {
        _attack = Resources.Load<Transform>("FrostBolt");
        EntityHealth.OnAnyEntityHit += FreezeEnemy;
    }

    // Register the attack in the EntityCombat to a Secondary slot and inject Cooldown multiplied by 0.7 to the power of (_amount - 1)
    void Start()
    {
        if (TryGetComponent<EntityCombat>(out EntityCombat ec))
        {
            _attackData = ec.RegisterAttack(EntityCombat.AttackSlot.SECONDARY, _attack);
            _attackData.Cooldown *= Mathf.Pow(0.7f, _amount - 1);
        }
    }

    // If enemy gets hit by attack named frostbolt, freeze them
    private void FreezeEnemy(object sender, HitData e)
    {
        if(e.DamageSource.gameObject.name == "FrostBolt(Clone)")
        {
            StartCoroutine(Freeze(e.DamageReceiver.gameObject));
        }    
    }

    // Save movement speed, drop it to 0 and change sprite to cyan, after 4 second bring it back to the original and change sprite to white
    private IEnumerator Freeze(GameObject enemy)
    {
        EntityMovement movement = enemy.GetComponent<EntityMovement>();
        SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
        float originalSpeed = movement.Speed;
        sprite.color = Color.cyan;
        movement.Speed = 0;
        yield return new WaitForSeconds(4);
        if(enemy != null)
        {
            movement.Speed = originalSpeed;
            sprite.color = Color.white;
        }

    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityHit -= FreezeEnemy;
    }

}
