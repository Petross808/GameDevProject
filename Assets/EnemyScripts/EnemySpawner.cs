using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform _enemyTemplate;
    public int _waveSize = 0;
    public float _timeBetweenWaves = 0;
    public float _timeBetweenUnits = 0;

    private float _unitTimer;
    private float _waveTimer;
    private int _unitsToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        _unitTimer = 0;
        _waveTimer = 5;
        _unitsToSpawn = _waveSize;
    }

    private void SpawnUnit()
    {
        Transform unit = Instantiate(_enemyTemplate,transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(_waveTimer > 0)
        {
            _waveTimer -= Time.deltaTime;
            return;
        }
        
        if(_unitsToSpawn <= 0)
        {
            _unitsToSpawn = _waveSize;
            _waveTimer = _timeBetweenWaves;
            return;
        }

        if(_unitTimer > 0)
        {
            _unitTimer -= Time.deltaTime;
        }
        else
        {
            SpawnUnit();
            _unitsToSpawn--;
            _unitTimer = _timeBetweenUnits;
        }    
    }
}
