using System;
using System.Collections;
using System.Collections.Generic;
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

    private float _unitTimer;

    void Awake()
    {
        _unitTimer = _unitSpawnTime;
    }

    private void SpawnUnit()
    {
        Vector2 randPos = UnityEngine.Random.insideUnitCircle.normalized * 30;
        Transform unit = Instantiate(_enemyTemplate, new Vector3(randPos.x, randPos.y, 1) ,new Quaternion(0,0,0,0));
        unit.GetComponent<AILogic>().Target = _enemyTarget;
    }

    // Update is called once per frame
    void Update()
    {
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
}
