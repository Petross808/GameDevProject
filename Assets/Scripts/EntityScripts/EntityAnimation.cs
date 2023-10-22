using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(SpriteRenderer))]
public class EntityAnimation : MonoBehaviour
{
    private Animator _animator;
    private EntityMovement _entityMovement;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _entityMovement = GetComponent<EntityMovement>();
        _entityMovement.OnEntityMove += RunAnimation;
        _entityMovement.OnEntityStop += IdleAnimation;
    }

    private void IdleAnimation(object sender, EventArgs e)
    {
        _animator.SetBool("Running", false);
    }

    private void RunAnimation(object sender, Vector2 dir)
    {
        _animator.SetBool("Running", true);
        _spriteRenderer.flipX = dir.x < 0;

    }

    private void OnDestroy()
    {
        _entityMovement.OnEntityMove -= RunAnimation;
        _entityMovement.OnEntityStop -= IdleAnimation;
    }
}
