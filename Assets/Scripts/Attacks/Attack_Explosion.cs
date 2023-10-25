using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class Attack_Explosion : MonoBehaviour, IAttack
{
    [SerializeField]
    private int _damage = 50;
    [SerializeField]
    private float _cooldown = 0.25f;
    [SerializeField]
    private bool _isHealing = false;

    private LayerMask _hitMask = 0;
    public int Damage { get => _damage; set => _damage = value; }
    public float Cooldown { get => _cooldown; set => _cooldown = value; }
    public LayerMask HitMask { get => _hitMask; set => _hitMask = value; }
    public bool IsHealing { get => _isHealing; set => _isHealing = value; }


    // Destroy collider after 0.1 seconds, destroy self when cooldown ends
    void Awake()
    {
        Collider collider = GetComponent<Collider>();
        Destroy(collider,0.1f);
        Destroy(this.gameObject, _cooldown);
    }

    public bool Attack(Transform aim)
    {
        throw new System.NotImplementedException();
    }

    public void CooldownTick()
    {
        throw new System.NotImplementedException();
    }
}
