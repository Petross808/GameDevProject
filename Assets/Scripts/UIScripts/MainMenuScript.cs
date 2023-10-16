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
    private Button _playButton;

    void Start()
    {
        _document = GetComponent<UIDocument>();

        _playButton = _document.rootVisualElement.Q<Button>("PlayButton");

        _playButton.RegisterCallback<ClickEvent>(StartGame);
    }

    private void StartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("GameScene");
    }

}
