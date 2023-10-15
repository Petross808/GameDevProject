using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData
{
    private int _damageDealt;
    private EntityHealth _receiver;
    private Hitbox _source;
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
