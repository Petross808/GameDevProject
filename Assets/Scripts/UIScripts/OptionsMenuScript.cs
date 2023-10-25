using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField]
    private MainMenuScript _menu;

    private UIDocument _document;
    private Slider _volumeSlider; // Open game options
    private Label _menuButton; // Quit the game

    // Initialize variables and register events
    void Awake()
    {
        _document = GetComponent<UIDocument>();

        _volumeSlider = _document.rootVisualElement.Q<Slider>("VolumeSlider");
        _menuButton = _document.rootVisualElement.Q<Label>("MenuButton");

        _volumeSlider.RegisterCallback<ChangeEvent<float>>(UpdateVolume);
        _menuButton.RegisterCallback<ClickEvent>(BackToMenu);

        _document.rootVisualElement.visible = false;
    }

    public void ShowUI()
    {
        _volumeSlider.value = AudioListener.volume;
        _document.rootVisualElement.visible = true;
    }

    private void BackToMenu(ClickEvent evt)
    {
        _document.rootVisualElement.visible = false;
        _menu.ShowUI();
    }

    private void UpdateVolume(ChangeEvent<float> evt)
    {
        AudioListener.volume = evt.newValue;
    }

}
