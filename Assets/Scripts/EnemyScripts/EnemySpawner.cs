using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _enemyTemplate;
    [SerializeField]
    [Range(0.01f, 120f)]
    private float _unitSpawnTime;
    [SerializeField]
    private float _spawnTimeVariance;
    [SerializeField]
    private float _deltaSpawnTimePerTenSeconds;
    [SerializeField]
    private GameObject _enemyTarget;


    [SerializeField]
    private int _burstSize;
    [SerializeField]
    private float _burstSpawnTime;
    [SerializeField]
    private int _enableBurstsAt;
    [SerializeField]
    private int _timeBetweenBurst;
    

    private bool _enabled = false;
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
            _enabled = true;
        }
    }

    private void ActivateBurst(object sender, int e)
    {
        if (!_enabled) return;
        if(e < _enableBurstsAt) return;

        if(e%_timeBetweenBurst == 0)
        {
            StartCoroutine(Burst());
        }
    }

    private void UpdateSpawnRate(object sender, int e)
    {
        if(e%10 == 0)
        {
            _unitSpawnTime *= _deltaSpawnTimePerTenSeconds;
        }
    }

    private void Disable(object sender, HitData e)
    {
        _enabled = false;
    }

    private void SpawnUnit()
    {
        Vector2 spawnPos = (UnityEngine.Random.insideUnitCircle.normalized * 50) - (Vector2)(_enemyTarget.transform.position);
        Transform unit = Instantiate(_enemyTemplate, new Vector3(spawnPos.x, spawnPos.y, 1) ,new Quaternion(0,0,0,0));
        unit.GetComponent<AILogic>().Target = _enemyTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enabled)
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
            if (!_enabled) break;
            SpawnUnit();
            yield return new WaitForSeconds(_burstSpawnTime);
        }
    }

    private void OnDestroy()
    {
        if(_enabled)
        {
            _targetEntityHealth.OnEntityDeath -= Disable;
            GameState.OnGameSecondPassed -= UpdateSpawnRate;
            GameState.OnGameSecondPassed -= ActivateBurst;
        }
    }
}
