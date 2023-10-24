using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameState _gameState;

    [SerializeField]
    private Transform _enemyTemplate; // What enemy is it spawning
    [SerializeField]
    private GameObject _enemyTarget; // What are the enemies following

    [Header("Spawn Times")]
    [SerializeField]
    [Range(0.01f, 120f)]
    private float _unitMaxSpawnTime; // Time between units spawn at the middle of the spawning period
    [SerializeField]
    [Range(0.01f, 120f)]
    private float _unitMinSpawnTime; // Time between units spawn at the beginning and the end of the spawning period
    [SerializeField]
    private float _spawnTimeVariance; // How much are the spawn times affected by randomness
    [SerializeField]
    private float _spawnStartTime; // When does the spawning period start
    [SerializeField]
    private float _spawnEndTime; // When does the spawning period end

    [Header("Bursts")]
    [SerializeField]
    private bool _enableBursts; // Do the enemies spawn in bursts
    [SerializeField]
    private int _burstSize; // How many enemies spawn in one burst
    [SerializeField]
    private float _burstSpawnTime; // Time between units spawn during the burst
    [SerializeField]
    private int _enableBurstsAt; // When should the bursts start
    [SerializeField]
    private int _timeBetweenBurst; // Time between two bursts


    private float _unitSpawnTime; // Time before the current next enemy spawns
    private bool _initialized = false; // Is there a valid target
    private bool _spawningEnabled = false; // Is spawning enabled
    private EntityHealth _targetEntityHealth; // EntityHealth component of the target

    private float _unitTimer;

    // Start timer, check if target is valid and register events
    void Awake()
    {
        _unitTimer = _unitSpawnTime;
        if(_enemyTarget.TryGetComponent<EntityHealth>(out _targetEntityHealth))
        {
            _targetEntityHealth.OnEntityDeath += Disable;
            _gameState.OnGameSecondPassed += UpdateSpawnRate;
            _gameState.OnGameSecondPassed += ActivateBurst;
            _initialized = true;
        }
    }

    // Check and start a burst if it should happen at this second
    private void ActivateBurst(object sender, int gameTime)
    {
        if (!_spawningEnabled) return;
        if (!_enableBursts) return;
        if(gameTime < _enableBurstsAt) return;

        if((gameTime + _enableBurstsAt) % _timeBetweenBurst == 0)
        {
            StartCoroutine(Burst());
        }
    }

    // Enable spawning if current time is in the spawning period, then update spawn rate
    private void UpdateSpawnRate(object sender, int gameTime)
    {
        if(gameTime > _spawnStartTime && gameTime < _spawnEndTime) 
        {
            _spawningEnabled = true;
            CalculateSpawnRate(gameTime);
        }
        else
        {
            _spawningEnabled = false;
        }
    }

    // Disable spawning if the target dies
    private void Disable(object sender, HitData e)
    {
        _spawningEnabled = false;
    }

    // If spawning enabled and target is valid, spawn an enemy 50 units from the target in a random direction, then inject the target into the enemy
    private void SpawnUnit()
    {
        if(!_spawningEnabled) return;
        if (_enemyTarget == null) return;

        Vector2 spawnPos = (UnityEngine.Random.insideUnitCircle.normalized * 50) - (Vector2)(_enemyTarget.transform.position);
        Transform unit = Instantiate(_enemyTemplate, new Vector3(spawnPos.x, spawnPos.y, 1) ,new Quaternion(0,0,0,0));
        unit.GetComponent<AILogic>().Target = _enemyTarget;
    }

    // If spawning is enabled, tick down timer and spawn a unit when timer reaches zero, then reset it
    void Update()
    {
        if (!_spawningEnabled)
        {
            return;
        }

        if (_unitTimer > 0)
        {
            _unitTimer -= Time.deltaTime;
        }
        else
        {
            SpawnUnit();
            _unitTimer = UnityEngine.Random.Range((1f - _spawnTimeVariance) * _unitSpawnTime, (1f + _spawnTimeVariance) * _unitSpawnTime);
        }
    }

    // Activate a burst - spawn a unit each _burstSpawnTime seconds _burstSize times
    private IEnumerator Burst()
    {
        for(int i = 0; i<_burstSize; i++)
        {
            if (!_spawningEnabled) break;
            SpawnUnit();
            yield return new WaitForSeconds(_burstSpawnTime);
        }
    }

    // Calculate the time before the next unit spawn at the specified second
    // The function is an upside down parabola where f(_spawnStartTime) = _unitMaxSpawnTime, f(_spawnEndTime) = _unitMaxSpawnTime, and peak in the middle at y = _unitMinSpawnTime
    private void CalculateSpawnRate(int time)
    {
        float x = (time - _spawnEndTime) / (_spawnStartTime - _spawnEndTime);
        _unitSpawnTime = _unitMinSpawnTime + (4 * x * (x - 1) + 1) * (_unitMaxSpawnTime - _unitMinSpawnTime) ;
    }

    private void OnDestroy()
    {
        if(_initialized)
        {
            _targetEntityHealth.OnEntityDeath -= Disable;
            _gameState.OnGameSecondPassed -= UpdateSpawnRate;
            _gameState.OnGameSecondPassed -= ActivateBurst;
        }
    }
}
