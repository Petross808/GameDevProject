using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

[RequireComponent(typeof(GameInput))]
public class GameState : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTemplate;

    private Transform _player;
    private IController _playerController;
    private GameInput _gameInput;

    [SerializeField]
    private float _gameTime;
    private int _gameSeconds;

    void Awake()
    {
        _gameInput = GetComponent<GameInput>();

        EntityLeveling.OnAnyEntityLevelUp += PauseGame;
        EntityInventory.OnAnyEntityGainItem += ResumeGame;
        EntityHealth.OnAnyEntityDeath += GameOver;

        SpawnPlayer();
        SetCameraFollowPlayer();
    }

    private void PauseGame(object sender, int e)
    {
        Time.timeScale = 0;
    }

    private void ResumeGame(object sender, ItemSO e)
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        ChangeController(_playerController);
        this.RaiseEvent(OnGameStart);
    }

    private void SpawnPlayer()
    {
        if (_player != null)
        {
            Destroy(_player);
        }
        _player = Instantiate(_playerTemplate);
    }

    private void SetCameraFollowPlayer()
    {
        ParentConstraint camConstraint;
        if(!Camera.main.TryGetComponent<ParentConstraint>(out camConstraint))
        {
            camConstraint = Camera.main.AddComponent<ParentConstraint>();
            camConstraint.AddSource(new ConstraintSource() { sourceTransform = _player, weight = 1 });
        }
        else 
        {
            camConstraint.SetSource(0, new ConstraintSource() { sourceTransform = _player, weight = 1 });
        }
        camConstraint.translationAxis = Axis.X | Axis.Y;
        camConstraint.constraintActive = true;
    }

    private void ChangeController(IController controller)
    {
        if(controller != null)
        {
            _gameInput.Controller = controller;
        }
    }

    public void GameOver(object sender, HitData data)
    {
        if(data.DamageReceiver.transform.root.CompareTag("Crystal"))
        {
            Time.timeScale = 0;
            this.RaiseEvent(OnGameEnd);
        }
    }

    public void CheckGameWon()
    {
        if(_gameSeconds == 600)
        {
            Time.timeScale = 0;
            this.RaiseEvent(OnGameWon);
        }
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        if(Mathf.FloorToInt(_gameTime) != _gameSeconds)
        {
            _gameSeconds = Mathf.FloorToInt(_gameTime);
            this.RaiseEvent<int>(OnGameSecondPassed, _gameSeconds);
            CheckGameWon();
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityDeath -= GameOver;
        EntityLeveling.OnAnyEntityLevelUp -= PauseGame;
        EntityInventory.OnAnyEntityGainItem -= ResumeGame;
    }

    public static event EventHandler OnGameStart;
    public static event EventHandler<int> OnGameSecondPassed;
    public static event EventHandler OnGameEnd;
    public static event EventHandler OnGameWon;
}
