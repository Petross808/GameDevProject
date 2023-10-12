using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField]
    private Transform _fill;

    private EntityHealth _entityHealth;

    void Awake()
    {
        gameObject.SetActive(false);
        _entityHealth = GetComponentInParent<EntityHealth>();
        if( _entityHealth != null )
        {
            _entityHealth.OnEntityHit += UpdateHealthBar;
        }
    }

    private void UpdateHealthBar(object sender, HitData data)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        _fill.transform.localScale = new(((float)_entityHealth.Health-data.DamageDealt)/_entityHealth.MaxHealth, _fill.transform.localScale.y, _fill.transform.localScale.z);
        _fill.transform.localPosition = new(-0.5f * (1- (((float)_entityHealth.Health - data.DamageDealt) / _entityHealth.MaxHealth)), _fill.transform.localPosition.y, _fill.transform.localPosition.z);
    }

    private void OnDestroy()
    {
        if (_entityHealth != null)
        {
            _entityHealth.OnEntityHit -= UpdateHealthBar;
        }
    }
}
