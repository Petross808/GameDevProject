using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameOverUIScript : MonoBehaviour
{
    private UIDocument _document;
    private Button _menuButton;

    void Start()
    {
        _document = GetComponent<UIDocument>();

        _menuButton = _document.rootVisualElement.Q<Button>("MenuButton");
        _menuButton.RegisterCallback<ClickEvent>(BackToMenu);

        GameState.OnGameEnd += ShowUI;

        _document.rootVisualElement.visible = false;
    }

    private void ShowUI(object sender, EventArgs e)
    {
        _document.rootVisualElement.visible = true;
    }

    private void BackToMenu(ClickEvent evt)
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnDestroy()
    {
        GameState.OnGameEnd -= ShowUI;
    }

}