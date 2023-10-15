using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UpgradeMenuScript : MonoBehaviour
{
    [SerializeField]
    private ItemPool _itemPool;

    private UIDocument _document;
    private VisualElement _root;
    private VisualElement[] _upgradeOptions;
    private ItemSO[] _items;

    private ItemSO _currentChoice;
    private EntityInventory _currentInventory;
    private Button _confirm;


    void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;
        _upgradeOptions = new VisualElement[]
        {
            _root.Q<VisualElement>("UpgradeOption1"),
            _root.Q<VisualElement>("UpgradeOption2"),
            _root.Q<VisualElement>("UpgradeOption3")
        };

        _confirm = _root.Q<Button>("Confirm");

        EntityLeveling.OnAnyEntityLevelUp += ShowUpgradeUI;

        _upgradeOptions[0].RegisterCallback<ClickEvent>((_)=> ChooseUpgrade(0));
        _upgradeOptions[1].RegisterCallback<ClickEvent>((_) => ChooseUpgrade(1));
        _upgradeOptions[2].RegisterCallback<ClickEvent>((_) => ChooseUpgrade(2));

        _confirm.RegisterCallback<ClickEvent>(ConfirmChoice);

        _root.visible = false;
    }

    private void ShowUpgradeUI(object sender, int e)
    {
        if (sender is EntityLeveling el &&
            el.gameObject.CompareTag("Player") &&
            el.gameObject.TryGetComponent<EntityInventory>(out EntityInventory inventory))
        {
            _currentInventory = inventory;
            LoadRandomItems();
            _confirm.SetEnabled(false);
            _root.visible = true;

        }
    }

    private void LoadRandomItems()
    {
        _items = new ItemSO[3];
        for (int i = 0; i < 3; i++)
        {
            _items[i] = _itemPool.GetRandomItem(_items);

            //VisualElement sprite = _upgradeOptions[i].Q<VisualElement>("Sprite");
            Label name = _upgradeOptions[i].Q<Label>("Name");
            Label description = _upgradeOptions[i].Q<Label>("Desc");
            if (_items[i] != null)
            {
                name.text = _items[i].ItemName;
                description.text = _items[i].ItemDescription;
            }
        }
    }

    private void ChooseUpgrade(int index)
    {
        foreach(var uo in _upgradeOptions)
        {
            uo.style.borderBottomColor = Color.black;
            uo.style.borderTopColor = Color.black;
            uo.style.borderLeftColor = Color.black;
            uo.style.borderRightColor = Color.black;
        }
        _upgradeOptions[index].style.borderBottomColor = Color.yellow;
        _upgradeOptions[index].style.borderTopColor = Color.yellow;
        _upgradeOptions[index].style.borderLeftColor = Color.yellow;
        _upgradeOptions[index].style.borderRightColor = Color.yellow;

        _currentChoice = _items[index];
        _confirm.SetEnabled(true);
    }

    private void ConfirmChoice(ClickEvent evt)
    {
        if(_currentInventory != null &&
            _currentChoice != null)
        {
            _currentInventory.AddItem(_currentChoice);
        }
        _confirm.SetEnabled(false);
        _root.visible = false;
    }

    private void OnDestroy()
    {
        EntityLeveling.OnAnyEntityLevelUp -= ShowUpgradeUI;
    }
}
