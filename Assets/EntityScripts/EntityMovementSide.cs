using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovementSide : MonoBehaviour
{
    public float _speed;
    public float _jumpSpeed;

    private Rigidbody2D _rb;
    private float _moveAxis;

    public float Speed { get => _speed; set => _speed = value; }
    public float JumpSpeed { get => _jumpSpeed; set => _jumpSpeed = value; }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveAxis = 0;
    }

    private void FixedUpdate()
    {
        _rb.velocityX = _speed * _moveAxis;
    }

    public void MoveStart(Vector2 direction)
    {
        RaiseOnEntityMove(direction);
        _moveAxis = direction.x;
    }

    public void MoveEnd()
    {
        RaiseOnEntityStop();
        _moveAxis = 0;
    }

    public void Jump()
    {
        if (isGrounded())
        {
            RaiseOnEntityJump();
            _rb.velocityY = _jumpSpeed;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.3f, 1 << LayerMask.NameToLayer("Ground"));
    }

    public event EventHandler<Vector2> OnEntityMove;
    protected virtual void RaiseOnEntityMove(Vector2 direction)
    {
        EventHandler<Vector2> handler = OnEntityMove;
        if (handler != null)
        {
            handler(this, direction);
        }
    }

    public event EventHandler OnEntityStop;
    protected virtual void RaiseOnEntityStop()
    {
        EventHandler handler = OnEntityStop;
        if (handler != null)
        {
            handler(this,null);
        }
    }

    public event EventHandler OnEntityJump;
    protected virtual void RaiseOnEntityJump()
    {
        EventHandler handler = OnEntityJump;
        if (handler != null)
        {
            handler(this, null);
        }
    }
}
