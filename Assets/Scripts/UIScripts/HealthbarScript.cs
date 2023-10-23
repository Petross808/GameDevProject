using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField]
    private Transform _fill;

    private EntityHealth _entityHealth;

    // If object has EntityHealth, register events
    void Awake()
    {
        gameObject.SetActive(false);
        _entityHealth = GetComponentInParent<EntityHealth>();
        if( _entityHealth != null )
        {
            _entityHealth.OnAfterEntityHit += UpdateHealthBar;
            _entityHealth.OnAfterEntityHeal += UpdateHealthBar;
        }
    }

    // When health of the entity changes, set Health Bar to active and update it's value, if full health, hide it again
    private void UpdateHealthBar(object sender, object args)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        _fill.transform.localScale = new((float)_entityHealth.Health/_entityHealth.MaxHealth, _fill.transform.localScale.y, _fill.transform.localScale.z);
        _fill.transform.localPosition = new(-0.5f * (1- ((float)_entityHealth.Health / _entityHealth.MaxHealth)), _fill.transform.localPosition.y, _fill.transform.localPosition.z);
        
        if((float)_entityHealth.Health / _entityHealth.MaxHealth == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (_entityHealth != null)
        {
            _entityHealth.OnAfterEntityHit -= UpdateHealthBar;
        }
    }
}
