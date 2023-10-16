using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attack_Aura : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _damage = 10;
    [SerializeField]
    private float _cooldown = 0.3f;

    private Collider2D _collider;
    private float _attackTime = 5/60f;
    private LayerMask _hitMask = 0;

    public int Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public LayerMask HitMask { get => _hitMask; set => _hitMask = value; }

    private float _activeTimer;
    private float _cdTimer;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _activeTimer = _attackTime;
        _cdTimer = 0;
    }

    public void Attack(Transform aim)
    {
        _collider.enabled = true;
        _activeTimer = _attackTime;
        _cdTimer = _cooldown;
    }

    void Update()
    {
        _activeTimer -= Time.deltaTime;
        CooldownTick();
        if (_activeTimer <= 0)
        {
            _collider.enabled = false;
        }
    }

    public void CooldownTick()
    {
        if (_cdTimer > 0)
        {
            _cdTimer -= Time.deltaTime;
        }
        else
        {
            Attack(null);
        }
    }
}
