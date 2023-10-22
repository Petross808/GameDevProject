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
    private Button _menuButton;
    private Label _label;

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

    private void ShowLossUI(object sender, EventArgs e)
    {
        _label.text = "You Lost";
        _document.rootVisualElement.visible = true;
    }
    private void ShowWinUI(object sender, EventArgs e)
    {
        _label.text = "You Won";
        _document.rootVisualElement.visible = true;
    }

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