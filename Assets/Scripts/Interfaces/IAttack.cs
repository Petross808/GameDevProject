using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    int Damage { get; set; }
    float Cooldown { get; set; }
    LayerMask HitMask { get; set; }

    bool IsHealing { get; set; }

    bool Attack(Transform aim);
    void CooldownTick();
}
