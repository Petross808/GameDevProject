using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    private string _itemName; // Display name of the item

    [SerializeField]
    [TextArea]
    private string _itemDescription; // Description of the item

    [SerializeField]
    private String _entityModifier; // Name of the script associated with this item

    public String EntityModifier { get => _entityModifier; }
    public string ItemName { get => _itemName; }
    public string ItemDescription { get => _itemDescription; }
}
