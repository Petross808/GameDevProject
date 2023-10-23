using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Ranged : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _damage = 100;
    [SerializeField]
    private float _cooldown = 5f;
    [SerializeField]
    private bool _isHealing = false;
    [SerializeField]
    private Transform _projectileTemplate;

    private LayerMask _hitMask = 0;
    public int Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public LayerMask HitMask { get => _hitMask; set => _hitMask = value; }
    public bool IsHealing { get => _isHealing; set => _isHealing = value; }

    private float _cdTimer = 0;

    // If not on cooldown, spawn projectile and inject transform data and hitbox component, reset cooldown
    public bool Attack(Transform aim)
    {
        if (_cdTimer <= 0)
        {
            Transform projectile = Instantiate(_projectileTemplate);
            if(projectile.TryGetComponent<Projectile>(out Projectile p))
            {
                p.Setup(GetComponent<Hitbox>(), aim.position, aim.rotation);
            }
            _cdTimer = _cooldown;
            return true;
        }
        return false;
    }

    // Tick down cooldown
    public void CooldownTick()
    {
        if (_cdTimer > 0)
        {
            _cdTimer -= Time.deltaTime;
        }
    }
}
