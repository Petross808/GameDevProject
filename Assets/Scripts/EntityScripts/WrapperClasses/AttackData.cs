using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data class for OnEntityAttack events
public class AttackData
{
    private EntityCombat _entityCombat; // EntityCombat that raised the event
    private IAttack _attack; // Attack that was used
    private EntityCombat.AttackSlot _slot; // Attack slot that was used
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
