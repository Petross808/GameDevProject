using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UpgradeMenuScript : MonoBehaviour
{
    [SerializeField]
    private ItemPool _commonItemPool;
    [SerializeField]
    private ItemPool _rareItemPool;
    [SerializeField]
    private ItemPool _legendaryItemPool;

    private UIDocument _document;
    private VisualElement _root;
    private VisualElement[] _upgradeOptions;
    private ItemSO[] _items;
    private Color[] _rarities;

    private ItemSO _currentChoice;
    private EntityInventory _currentInventory;
    private Label _confirm;

    // Initialize variables and register events, then hide the UI
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

        _confirm = _root.Q<Label>("Confirm");

        EntityLeveling.OnAnyEntityLevelUp += ShowUpgradeUI;
        _upgradeOptions[0].RegisterCallback<ClickEvent>(ChooseFirst);
        _upgradeOptions[1].RegisterCallback<ClickEvent>(ChooseSecond);
        _upgradeOptions[2].RegisterCallback<ClickEvent>(ChooseThird);
        _confirm.RegisterCallback<ClickEvent>(ConfirmChoice);

        _root.visible = false;
    }

    // When the Player levels up, load random items and show the UI
    private void ShowUpgradeUI(object sender, int e)
    {
        if (sender is EntityLeveling el &&
            el.gameObject.CompareTag("Player") &&
            el.gameObject.TryGetComponent<EntityInventory>(out EntityInventory inventory))
        {
            _currentInventory = inventory;
            LoadItems();
            _confirm.SetEnabled(false);
            _root.visible = true;

        }
    }

    // Fill the arrays with random items and save colors based on their rarities
    private void GetRandomItems()
    {
        _items = new ItemSO[3];
        _rarities = new Color[3];
        for (int i = 0; i < 3; i++)
        {
            switch(UnityEngine.Random.Range(0, 100))
            {
                case 0:
                    _items[i] = _legendaryItemPool.GetRandomItem(_items);
                    _rarities[i] = Color.yellow;
                    break;
                case <= 10:
                    _items[i] = _rareItemPool.GetRandomItem(_items);
                    _rarities[i] = Color.cyan;
                    break;
                default:
                    _items[i] = _commonItemPool.GetRandomItem(_items);
                    _rarities[i] = Color.green;
                    break;
            }
        }
    }

    // Load names and descriptions of the items, change the color of the names to match their rarities
    private void LoadItems()
    {
        GetRandomItems();
        for (int i = 0; i < 3; i++)
        {
            //VisualElement sprite = _upgradeOptions[i].Q<VisualElement>("Sprite");
            Label name = _upgradeOptions[i].Q<Label>("Name");
            Label description = _upgradeOptions[i].Q<Label>("Desc");
            if (_items[i] != null)
            {
                name.text = _items[i].ItemName;
                name.style.color = _rarities[i];
                description.text = _items[i].ItemDescription;
            }
        }
    }

    // When an item is clicked, highlight it, save the choice and enable the confirm button
    private void ChooseUpgrade(int index)
    {
        foreach(var uo in _upgradeOptions)
        {
            SetBorderColor(uo, 0);
        }
        SetBorderColor(_upgradeOptions[index], 5);

        _currentChoice = _items[index];
        _confirm.SetEnabled(true);
    }

    private void ChooseFirst(ClickEvent e) { ChooseUpgrade(0); }
    private void ChooseSecond(ClickEvent e) { ChooseUpgrade(1); }
    private void ChooseThird(ClickEvent e) { ChooseUpgrade(2); }

    // When confirm is clicked, add the selected item into the inventory, reset all highlights and hide the UI
    private void ConfirmChoice(ClickEvent evt)
    {
        if(_currentInventory != null &&
            _currentChoice != null)
        {
            _currentInventory.AddItem(_currentChoice);
        }

        foreach (var uo in _upgradeOptions)
        {
            SetBorderColor(uo, 0);
        }
        _currentChoice = null;
        _confirm.SetEnabled(false);
        _root.visible = false;
    }

    // Set border color of a visual element
    private void SetBorderColor(VisualElement ve, int value) 
    {
        ve.style.borderBottomWidth = value;
        ve.style.borderTopWidth = value;
        ve.style.borderLeftWidth = value;
        ve.style.borderRightWidth = value;
    }

    private void OnDestroy()
    {
        EntityLeveling.OnAnyEntityLevelUp -= ShowUpgradeUI;

        _upgradeOptions[0].UnregisterCallback<ClickEvent>(ChooseFirst);
        _upgradeOptions[1].UnregisterCallback<ClickEvent>(ChooseSecond);
        _upgradeOptions[2].UnregisterCallback<ClickEvent>(ChooseThird);
        _confirm.UnregisterCallback<ClickEvent>(ConfirmChoice);
    }
}
