using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    private int _gameTime = 0;  // Time survived
    private int _playerLevel= 1; // Player level
    private int _enemiesKilled = 0; // Enemies killed
    private int _damageDealt = 0; // Total damage dealt
    private int _damageReceived = 0; // Total damage received

    public int GameTime => _gameTime;
    public int PlayerLevel  => _playerLevel;
    public int EnemiesKilled => _enemiesKilled;
    public int DamageDealt => _damageDealt;
    public int DamageReceived => _damageReceived;

    void Start()
    {
        GameState.OnGameSecondPassed += UpdateTime;
        EntityLeveling.OnAnyEntityLevelUp += PlayerLevelUp;
        EntityHealth.OnAnyEntityDeath += EnemyKilled;
        EntityHealth.OnAfterAnyEntityHit += EnemyReceivedDamage;
        EntityHealth.OnAfterAnyEntityHit += ShipReceivedDamage;
    }

    // When the Ship is dealt damage, add it to received damage
    private void ShipReceivedDamage(object sender, int e)
    {
        if (sender is EntityHealth eh &&
            eh.CompareTag("Ship"))
        {
            _damageReceived += e;
        }
    }

    // When an Enemy is dealt damage, add it to damage dealt
    private void EnemyReceivedDamage(object sender, int e)
    {
        if(sender is EntityHealth eh &&
            eh.CompareTag("Enemy"))
        {
            _damageDealt += e;
        }
    }

    // When an Enemy is killed, increment enemies killed
    private void EnemyKilled(object sender, HitData e)
    {
        if(e.DamageReceiver.CompareTag("Enemy"))
        {
            _enemiesKilled++;
        }
    }

    // When the Player levels up, update player level
    private void PlayerLevelUp(object sender, int e)
    {
        if (sender is EntityLeveling el &&
            el.CompareTag("Player"))
        {
            _playerLevel = e;
        }
    }

    // When a game second passes, update game time
    private void UpdateTime(object sender, int e)
    {
        _gameTime = e;
    }

    private void OnDestroy()
    {
        GameState.OnGameSecondPassed -= UpdateTime;
        EntityLeveling.OnAnyEntityLevelUp -= PlayerLevelUp;
        EntityHealth.OnAnyEntityDeath -= EnemyKilled;
        EntityHealth.OnAfterAnyEntityHit -= EnemyReceivedDamage;
        EntityHealth.OnAfterAnyEntityHit -= ShipReceivedDamage;
    }
}
