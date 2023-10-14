using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private MonoScript _entityModifier;

    public int Id { get => _id; }
    public MonoScript EntityModifier { get => _entityModifier; }
}
