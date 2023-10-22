using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    private EntityCombat _entityCombat;
    private IAttack _attack;
    private EntityCombat.AttackSlot _slot;
    public AttackData(EntityCombat user, IAttack attack, EntityCombat.AttackSlot slot)
    {
        _entityCombat = user;
        _attack = attack;
        _slot = slot;
    }

    public EntityCombat EntityCombat { get => _entityCombat; set => _entityCombat = value; }
    public IAttack Attack { get => _attack; set => _attack = value; }
    public EntityCombat.AttackSlot Slot { get => _slot; set => _slot = value; }
}
