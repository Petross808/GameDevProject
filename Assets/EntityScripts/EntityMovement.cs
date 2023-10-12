using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        this.RaiseEvent<Vector2>(OnEntityMove, direction);
        _moveVector = direction;
    }

    public void MoveEnd()
    {
        this.RaiseEvent(OnEntityStop);
        _moveVector = new(0, 0);
    }

    public event EventHandler<Vector2> OnEntityMove;
    public event EventHandler OnEntityStop;
}
