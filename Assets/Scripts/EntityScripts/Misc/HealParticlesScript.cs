using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealParticlesScript : MonoBehaviour
{
    private EntityHealth _entityHealth;
    private ParticleSystem _particleSystem;
    void Awake()
    {
        _entityHealth = GetComponentInParent<EntityHealth>();
        _particleSystem = GetComponent<ParticleSystem>();

        if( _entityHealth != null )
        {
            _entityHealth.OnAfterEntityHeal += PlayParticles;
        }
    }

    private void PlayParticles(object sender, int e)
    {
        _particleSystem.Play();
    }

    private void OnDestroy()
    {
        if (_entityHealth != null)
        {
            _entityHealth.OnAfterEntityHeal -= PlayParticles;
        }
    }

}
