using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attack interface
public interface IAttack
{
    int Damage { get; set; } // Damage of the attack
    float Cooldown { get; set; } // Cooldown of the attack
    LayerMask HitMask { get; set; } // What can be hit by the attack

    bool IsHealing { get; set; } // Does the attack heal instead of doing damage

    bool Attack(Transform aim); // Trigger the attack
    void CooldownTick(); // Tick down the cooldown of the attack
}
