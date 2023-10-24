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
    private IController _nullController;
    private GameInput _gameInput;

    [SerializeField] // Serialized for debug purposes
    private float _gameTime; // Exact game time
    private int _gameSeconds; // Game time in seconds

    // Initialize variables, register events, spawn the player and set the camera to follow them, reset timeScale
    void Awake()
    {
        _gameInput = GetComponent<GameInput>();

        EntityLeveling.OnAnyEntityLevelUp += PauseGameOnLevelUp;
        EntityInventory.OnAnyEntityGainItem += ResumeGameOnGainItem;
        EntityHealth.OnAnyEntityDeath += GameOver;

        SpawnPlayer();
        SetCameraFollowPlayer();
    }
    // Assign controllers and ResumeGame, setting timeScale to 1 and changing GameInput controller to _playerController, then raise OnGameStart event
    private void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _nullController = new NullController();
        ResumeGame();
        this.RaiseEvent(OnGameStart);
    }

    // Set time scale to 0 and controller to _nullController ignoring all input
    private void PauseGame()
    {
        Time.timeScale = 0;
        ChangeController(_nullController);
    }

    // Set time scale to 1 and controller to _playerContoller accepting all input
    private void ResumeGame()
    {
        Time.timeScale = 1;
        ChangeController(_playerController);
    }

    // When the player levels up, pause the game
    private void PauseGameOnLevelUp(object sender, int e)
    {
        if(sender is EntityLeveling el &&
            el.gameObject.CompareTag("Player"))
        {
            PauseGame();
        }
    }

    // When the player gets an item, resume the game
    private void ResumeGameOnGainItem(object sender, ItemSO e)
    {
        if (sender is EntityInventory el &&
            el.gameObject.CompareTag("Player"))
        {
            ResumeGame();
        }
    }

    // Instantiate the player
    private void SpawnPlayer()
    {
        if (_player != null)
        {
            Destroy(_player);
        }
        _player = Instantiate(_playerTemplate);
    }

    // If camera doesn't have a parentConstraint component, add it, then set it to follow the player
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

    // Change the controller that receives input
    private void ChangeController(IController controller)
    {
        if(controller != null)
        {
            _gameInput.Controller?.OnSwitch();
            _gameInput.Controller = controller;
        }
    }

    // When the ship dies, pause the game and raise OnGameEnd event
    public void GameOver(object sender, HitData data)
    {
        if(data.DamageReceiver.transform.root.CompareTag("Ship"))
        {
            PauseGame();
            this.RaiseEvent(OnGameEnd, GameEnd.LOSS);
        }
    }

    // If the timer reached 10 minutes, pause the game and raise OnGameWon event
    public void CheckGameWon()
    {
        if(_gameSeconds == 600)
        {
            PauseGame();
            this.RaiseEvent(OnGameEnd, GameEnd.WIN);
        }
    }

    // Increase the game time, if a second passed raise OnGameSecondPassed, then check if game is won
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
        EntityLeveling.OnAnyEntityLevelUp -= PauseGameOnLevelUp;
        EntityInventory.OnAnyEntityGainItem -= ResumeGameOnGainItem;
    }

    public enum GameEnd { WIN, LOSS}

    public static event EventHandler OnGameStart;
    public static event EventHandler<int> OnGameSecondPassed;
    public static event EventHandler<GameEnd> OnGameEnd;
}
