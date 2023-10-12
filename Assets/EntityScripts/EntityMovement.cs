using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rb;
    private Vector2 _moveVector;

    public float Speed { get => _speed; set => _speed = value; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveVector = new(0,0);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _speed * _moveVector;
    }

    public void MoveStart(Vector2 direction)
    {
        RaiseOnEntityMove(direction);
        _moveVector = direction;
    }

    public void MoveEnd()
    {
        RaiseOnEntityStop();
        _moveVector = new(0, 0);
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
}
