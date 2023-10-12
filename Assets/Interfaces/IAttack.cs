using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    int Damage { get; set; }
    float Cooldown { get; set; }
    LayerMask HitMask { get; }

    void Attack(Transform aim);
    void CooldownTick();
}
