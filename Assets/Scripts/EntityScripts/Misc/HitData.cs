using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data class for OnEntityHit events
public class HitData
{
    private int _damageDealt; // Amount of damage dealt
    private EntityHealth _receiver; // EntityHealth that received the damage
    private Hitbox _source; // Hitbox that dealt the damage
    public HitData(int damage, Hitbox source, EntityHealth receiver)
    {
        _damageDealt = damage;
        _receiver = receiver;
        _source = source;
    }

    public int DamageDealt { get => _damageDealt; set => _damageDealt = value; }
    public EntityHealth DamageReceiver { get => _receiver; set => _receiver = value; }
    public Hitbox DamageSource { get => _source; set => _source = value; }
}
