using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameState _gameState;
    [SerializeField]
    private GameInput _gameInput;

    private UIDocument _document;
    private Label _resumeButton;
    private Label _menuButton;

    // Initialize variables, register callbacks, hide UI
    void Start()
    {
        _document = GetComponent<UIDocument>();
        _resumeButton = _document.rootVisualElement.Q<Label>("ResumeButton");
        _menuButton = _document.rootVisualElement.Q<Label>("MenuButton");

        _resumeButton.RegisterCallback<ClickEvent>(ResumeGame);
        _menuButton.RegisterCallback<ClickEvent>(GoToMenu);

        _gameInput.OnEscapePressed += TogglePauseMenu;

        _document.rootVisualElement.visible = false;
    }

    // When escape button is pressed, if UI is hidden, show it and pause, otherwise hide it and unpause
    public void TogglePauseMenu(object sender, EventArgs e)
    {
        if(!_document.rootVisualElement.visible)
        {
            _gameState.PauseGame();
            _document.rootVisualElement.visible = true;
        }
        else
        {
            _gameState.ResumeGame();
            _document.rootVisualElement.visible = false;
        }
    }

    // When menu button is clicked, switch scene to main menu
    private void GoToMenu(ClickEvent evt)
    {
        SceneManager.LoadScene("MenuScene");
    }

    // When resume button is clicked, unpause the game and hide the UI
    private void ResumeGame(ClickEvent evt)
    {
        _gameState.ResumeGame();
        _document.rootVisualElement.visible = false;
    }

    private void OnDestroy()
    {
        _resumeButton.UnregisterCallback<ClickEvent>(ResumeGame);
        _menuButton.UnregisterCallback<ClickEvent>(GoToMenu);

        _gameInput.OnEscapePressed -= TogglePauseMenu;
    }

}
