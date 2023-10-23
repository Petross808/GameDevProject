using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0; // Speed of the projectile
    [SerializeField]
    private float _lifetime = 0; // How long before the projectile disappears

    private Hitbox _hitboxScript; // Hitbox script of the attack that fired this projectile
    private Rigidbody2D _rb;

    // Destroy after _lifetime seconds
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, _lifetime);   
    }

    // Setup function for the projectile, used by the attack to inject inital values
    public void Setup(Hitbox hitboxScript, Vector3 position, Quaternion rotation)
    {
        _hitboxScript = hitboxScript;
        transform.position = position;
        transform.rotation = rotation;
    }

    // Move forward every frame
    void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed;
    }

    // When collision happens, call the attack's collision function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hitboxScript != null) 
        {
            _hitboxScript.OnTriggerEnter2D(collision);
        }
    }
}
