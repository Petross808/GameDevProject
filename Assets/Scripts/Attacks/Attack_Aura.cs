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
    [SerializeField]
    private bool _isHealing = false;

    private Collider2D _collider;
    private float _attackTime = 5/60f;
    private LayerMask _hitMask = 0;

    public int Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public LayerMask HitMask { get => _hitMask; set => _hitMask = value; }
    public bool IsHealing { get => _isHealing; set => _isHealing = value; }

    private float _activeTimer = 0;
    private float _cdTimer = 0;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Enable collider and reset timers
    public bool Attack(Transform aim)
    {
        _collider.enabled = true;
        _activeTimer = _attackTime;
        _cdTimer = _cooldown;
        return true;
    }

    // Tick down timers, disable collider when active timer ends
    void Update()
    {
        _activeTimer -= Time.deltaTime;
        CooldownTick();
        if (_activeTimer <= 0)
        {
            _collider.enabled = false;
        }
    }

    // Tick down cooldown, call attack when cooldown ends
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
