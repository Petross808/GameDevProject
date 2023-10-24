using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIDocument))]
public class MainMenuScript : MonoBehaviour
{
    private UIDocument _document;
    private Label _playButton; // Start the game
    private Label _optionsButton; // Open game options
    private Label _quitButton; // Quit the game

    // Initialize variables and register events
    void Awake()
    {
        _document = GetComponent<UIDocument>();

        _playButton = _document.rootVisualElement.Q<Label>("PlayButton");
        _optionsButton = _document.rootVisualElement.Q<Label>("OptionsButton");
        _quitButton = _document.rootVisualElement.Q<Label>("QuitButton");

        _playButton.RegisterCallback<ClickEvent>(StartGame);
        _quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    // When the play button is clicked, switch the scene to the GameScene
    private void StartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("GameScene");
    }

    // When the quit button is clicked, close the game
    private void QuitGame(ClickEvent evt)
    {
        Application.Quit();
    }

}
