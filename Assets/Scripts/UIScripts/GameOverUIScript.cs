using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameOverUIScript : MonoBehaviour
{
    [SerializeField]
    private StatsTracker _stats;

    private UIDocument _document;
    private Label _menuButton; // Go back to menu button
    private Label _title; // You Won / You Lost
    private Label _time; // Game time field
    private Label _level; // Player level field
    private Label _kills; // Enemies killed field
    private Label _damageDealt; // Total damage dealt field
    private Label _damageReceived; // Total damage received field

    // Initialize variables, register events, hide the UI
    void Start()
    {
        _document = GetComponent<UIDocument>();

        _title = _document.rootVisualElement.Q<Label>("GameOverLabel");
        _menuButton = _document.rootVisualElement.Q<Label>("MenuButton");
        _time = _document.rootVisualElement.Q<Label>("TimeSurvived");
        _level = _document.rootVisualElement.Q<Label>("Level");
        _kills = _document.rootVisualElement.Q<Label>("EnemiesKilled");
        _damageDealt = _document.rootVisualElement.Q<Label>("DamageDealt");
        _damageReceived = _document.rootVisualElement.Q<Label>("DamageSurvived");


        _menuButton.RegisterCallback<ClickEvent>(BackToMenu);
        GameState.OnGameEnd += ShowUI;

        _document.rootVisualElement.visible = false;
    }

    // Show the UI and set the label to "You Lost"
    private void ShowUI(object sender, GameState.GameEnd e)
    {
        _title.text = (e == GameState.GameEnd.WIN) ? "You Won" : "You Lost";
        _time.text = String.Format("Time survived: {0:D2}:{1:D2}", (_stats.GameTime / 60), (_stats.GameTime % 60));
        _level.text = String.Format("Level: {0}", _stats.PlayerLevel);
        _kills.text = String.Format("Enemies killed: {0}", _stats.EnemiesKilled);
        _damageDealt.text = String.Format("Total damage dealt: {0}", _stats.DamageDealt);
        _damageReceived.text = String.Format("Total damage received: {0}", _stats.DamageReceived);
        _document.rootVisualElement.visible = true;
    }


    // When Menu button clicked, switch scenes to MainMenu
    private void BackToMenu(ClickEvent evt)
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnDestroy()
    {
        _menuButton.UnregisterCallback<ClickEvent>(BackToMenu);
        GameState.OnGameEnd -= ShowUI;
    }

}