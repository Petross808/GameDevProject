using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityInventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemSO> _startingItemList;

    public List<ItemSO> ItemList { get => _startingItemList; }

    // Initialize all items that started in the inventory
    void Start()
    {
        foreach(ItemSO item in _startingItemList)
        {
            AddItem(item);
        }
    }

    // Raise OnEntityGainItem events, then if this entity doesn't have the item component yet, add it, otherwise call it's AddAnother function
    public void AddItem(ItemSO item)
    {
        this.RaiseEvent<ItemSO>(OnEntityGainItem, item);
        this.RaiseEvent<ItemSO>(OnAnyEntityGainItem, item);

        System.Type entityModType = Type.GetType(item.EntityModifier);

        if(gameObject.TryGetComponent(entityModType, out Component component))
        {
            if(component is IEntityModifier entityModifier)
            {
                entityModifier.AddAnother();
            }
        }
        else
        {
            gameObject.AddComponent(entityModType);
        }
    }

    public event EventHandler<ItemSO> OnEntityGainItem;
    public static event EventHandler<ItemSO> OnAnyEntityGainItem;

}
