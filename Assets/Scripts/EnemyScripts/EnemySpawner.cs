using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyTemplate;
    [SerializeField]
    private GameObject _enemyTarget;

    [Header("Spawn Times")]
    [SerializeField]
    [Range(0.01f, 120f)]
    private float _unitMaxSpawnTime;
    [SerializeField]
    [Range(0.01f, 120f)]
    private float _unitMinSpawnTime;
    [SerializeField]
    private float _spawnTimeVariance;
    [SerializeField]
    private float _spawnStartTime;
    [SerializeField]
    private float _spawnEndTime;

    [Header("Bursts")]
    [SerializeField]
    private bool _enableBursts;
    [SerializeField]
    private int _burstSize;
    [SerializeField]
    private float _burstSpawnTime;
    [SerializeField]
    private int _enableBurstsAt;
    [SerializeField]
    private int _timeBetweenBurst;


    private float _unitSpawnTime;
    private bool _initialized = false;
    private bool _spawningEnabled = false;
    private EntityHealth _targetEntityHealth;

    private float _unitTimer;

    void Awake()
    {
        _unitTimer = _unitSpawnTime;
        if(_enemyTarget.TryGetComponent<EntityHealth>(out _targetEntityHealth))
        {
            _targetEntityHealth.OnEntityDeath += Disable;
            GameState.OnGameSecondPassed += UpdateSpawnRate;
            GameState.OnGameSecondPassed += ActivateBurst;
            _initialized = true;
        }
    }

    private void ActivateBurst(object sender, int e)
    {
        if (!_spawningEnabled) return;
        if (!_enableBursts) return;
        if(e < _enableBurstsAt) return;

        if(e%_timeBetweenBurst == 0)
        {
            StartCoroutine(Burst());
        }
    }

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

    private void Disable(object sender, HitData e)
    {
        _spawningEnabled = false;
    }

    private void SpawnUnit()
    {
        if(!_spawningEnabled) return;

        Vector2 spawnPos = (UnityEngine.Random.insideUnitCircle.normalized * 50) - (Vector2)(_enemyTarget.transform.position);
        Transform unit = Instantiate(_enemyTemplate, new Vector3(spawnPos.x, spawnPos.y, 1) ,new Quaternion(0,0,0,0));
        unit.GetComponent<AILogic>().Target = _enemyTarget;
    }

    // Update is called once per frame
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

    private IEnumerator Burst()
    {
        for(int i = 0; i<_burstSize; i++)
        {
            if (!_spawningEnabled) break;
            SpawnUnit();
            yield return new WaitForSeconds(_burstSpawnTime);
        }
    }

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
            GameState.OnGameSecondPassed -= UpdateSpawnRate;
            GameState.OnGameSecondPassed -= ActivateBurst;
        }
    }
}
