using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    private string _itemName;

    [SerializeField]
    [TextArea]
    private string _itemDescription;

    [SerializeField]
    private String _entityModifier;

    public String EntityModifier { get => _entityModifier; }
    public string ItemName { get => _itemName; }
    public string ItemDescription { get => _itemDescription; }
}
