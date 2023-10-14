using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityInventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemScriptableObject> _itemList;

    void Start()
    {
        foreach(ItemScriptableObject item in _itemList)
        {
            AddItem(item);
        }
    }

    public void AddItem(ItemScriptableObject item)
    {
        System.Type entityModType = item.EntityModifier.GetClass();

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
}
