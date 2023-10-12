using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwingAttack : MonoBehaviour, IAttack
{
    public int _damage = 50;
    public float _cooldown = 30 / 60f;
    public LayerMask _hitMask = 0;

    public int Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public LayerMask HitMask { get => _hitMask; }

    private float _attackTime;
    private float _activeTimer;
    private float _cdTimer;


    // Start is called before the first frame update
    void Start()
    {
        _attackTime = 5 / 60f;
        _activeTimer = _attackTime;
        _cdTimer = _cooldown;
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

    // Update is called once per frame
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
