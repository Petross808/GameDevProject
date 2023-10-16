using System;
using System.Collections;
using System.Collections.Generic;
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
    private GameObject _enemyTarget;

    private bool _enabled = false;
    private EntityHealth _targetEntityHealth;


    private float _unitTimer;

    void Awake()
    {
        _unitTimer = _unitSpawnTime;
        if(_enemyTarget.TryGetComponent<EntityHealth>(out _targetEntityHealth))
        {
            _targetEntityHealth.OnEntityDeath += Disable;
            _enabled = true;
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

    private void OnDestroy()
    {
        if(_enabled)
        {
            _targetEntityHealth.OnEntityDeath += Disable;
        }
    }
}
