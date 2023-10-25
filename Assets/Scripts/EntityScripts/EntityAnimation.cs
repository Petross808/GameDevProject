using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EntityAnimation : MonoBehaviour
{
    private Animator _animator;
    private EntityMovement _entityMovement;
    private EntityCombat _entityCombat;
    private EntityHealth _entityHealth;
    private SpriteRenderer _spriteRenderer;

    // Initialize variables and register callbacks
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _entityMovement = GetComponent<EntityMovement>();
        _entityCombat = GetComponent<EntityCombat>();
        _entityHealth = GetComponent<EntityHealth>();

        if( _entityMovement != null )
        {
            _entityMovement.OnEntityMove += RunAnimation;
            _entityMovement.OnEntityStop += IdleAnimation;
        }

        if( _entityCombat != null ) 
        {
            _entityCombat.OnEntityAttack += AttackAnimation;
        }
        
        if( _entityHealth != null )
        {
            _entityHealth.OnAfterEntityHit += HitAnimation;
        }

        if(TryGetComponent<EntityCorpse>(out EntityCorpse ec))
        {
            ec.Animator = _animator.runtimeAnimatorController;
        }
    }

    // When this entity gets hit, set the animator trigger hit
    private void HitAnimation(object sender, int e)
    {
        _animator.ResetTrigger("Hit");
        _animator.SetTrigger("Hit");
    }

    // When this entity attacks, set the animator trigger attack
    private void AttackAnimation(object sender, AttackData e)
    {
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("Attack");
    }

    // When this entity stops moving, set the animator boolean running to false
    private void IdleAnimation(object sender, EventArgs e)
    {
        _animator.SetBool("Running", false);
    }

    // When this entity starts moving, set the animator boolean running to true and flip the sprite if it's moving left
    private void RunAnimation(object sender, Vector2 dir)
    {
        _animator.SetBool("Running", true);
        _spriteRenderer.flipX = dir.x < 0;

    }

    private void OnDestroy()
    {
        if (_entityMovement != null)
        {
            _entityMovement.OnEntityMove -= RunAnimation;
            _entityMovement.OnEntityStop -= IdleAnimation;
        }

        if (_entityCombat != null)
        {
            _entityCombat.OnEntityAttack -= AttackAnimation;
        }

        if (_entityHealth != null)
        {
            _entityHealth.OnAfterEntityHit -= HitAnimation;
        }
    }
}
