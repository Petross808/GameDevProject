using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemPool")]
public class ItemPool : ScriptableObject
{
    [SerializeField]
    private List<ItemSO> _itemPool; // List of the items in this pool

    // Get a random item from this pool, excluding the items specified
    public ItemSO GetRandomItem(IEnumerable<ItemSO> exclude = null)
    {
        List<ItemSO> selectFrom = (exclude != null) ? _itemPool.Except(exclude).ToList() : _itemPool; 
        int randomIndex = UnityEngine.Random.Range(0, selectFrom.Count);
        return selectFrom[randomIndex];
    }

}
