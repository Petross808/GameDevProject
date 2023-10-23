using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameOverUIScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _menuButton; // Go back to menu
    private Label _label; // You Won / You Lost

    // Initialize variables, register events, hide the UI
    void Start()
    {
        _document = GetComponent<UIDocument>();

        _label = _document.rootVisualElement.Q<Label>("GameOverLabel");
        _menuButton = _document.rootVisualElement.Q<Button>("MenuButton");
        _menuButton.RegisterCallback<ClickEvent>(BackToMenu);

        GameState.OnGameEnd += ShowLossUI;
        GameState.OnGameWon += ShowWinUI;

        _document.rootVisualElement.visible = false;
    }

    // Show the UI and set the label to "You Lost"
    private void ShowLossUI(object sender, EventArgs e)
    {
        _label.text = "You Lost";
        _document.rootVisualElement.visible = true;
    }

    // Show the UI and set the label to "You Won"
    private void ShowWinUI(object sender, EventArgs e)
    {
        _label.text = "You Won";
        _document.rootVisualElement.visible = true;
    }

    // When Menu button clicked, switch scenes to MainMenu
    private void BackToMenu(ClickEvent evt)
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnDestroy()
    {
        GameState.OnGameEnd -= ShowLossUI;
        GameState.OnGameWon -= ShowWinUI;
    }

}