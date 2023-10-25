using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class Attack_MeleeDelayed : Attack_Melee
{
    [SerializeField]
    protected float _delay;

    protected float _delayTimer = 0;
    protected bool _waitingForDelay = false;
    protected Transform _aim;
    public override bool Attack(Transform aim)
    {
        if (_cdTimer <= 0)
        {
            _aim = aim;
            StartDelayTimer();
            _cdTimer = _cooldown;
            return true;
        }
        return false;
    }

    private void StartDelayTimer()
    {
        _delayTimer = _delay;
        _waitingForDelay = true;
    }

    private void DelayedAttack(Transform aim)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(aim.position, aim.rotation);
        _activeTimer = _attackTime;
    }

    public override void CooldownTick()
    {
        if (_cdTimer > 0)
        {
            _cdTimer -= Time.deltaTime;
        }

        if( _waitingForDelay )
        {
            _delayTimer -= Time.deltaTime;
            if(_delayTimer < 0)
            {
                DelayedAttack(_aim);
                _waitingForDelay = false;
            }
        }
    }
}
