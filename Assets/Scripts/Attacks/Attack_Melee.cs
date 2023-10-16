using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Melee: MonoBehaviour, IAttack
{
    [SerializeField]
    private int _damage = 50;
    [SerializeField]
    private float _cooldown = 30 / 60f;
    [SerializeField]
    private float _attackTime = 5 / 60f;

    private LayerMask _hitMask = 0;

    public int Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public LayerMask HitMask { get => _hitMask; set => _hitMask = value; }

    private float _activeTimer;
    private float _cdTimer;

    void Awake()
    {
        _activeTimer = _attackTime;
        _cdTimer = 0;
    }

    public void Attack(Transform aim)
    {
        if(_cdTimer <= 0)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(aim.position, aim.rotation);
            _activeTimer = _attackTime;
            _cdTimer = _cooldown;
        }
    }

    void Update()
    {
        _activeTimer -= Time.deltaTime;
        if (_activeTimer <= 0) 
        {
            gameObject.SetActive(false);
        }
    }

    public void CooldownTick()
    {
        if (_cdTimer > 0)
        {
            _cdTimer -= Time.deltaTime;
        }
    }
}
