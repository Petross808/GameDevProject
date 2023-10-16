using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0;
    [SerializeField]
    private float _lifetime = 0;

    private Hitbox _hitboxScript;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, _lifetime);   
    }

    public void Setup(Hitbox hitboxScript, Vector3 position, Quaternion rotation)
    {
        _hitboxScript = hitboxScript;
        transform.position = position;
        transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hitboxScript != null) 
        {
            _hitboxScript.OnTriggerEnter2D(collision);
        }
    }
}
