using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySound : MonoBehaviour
{
    [SerializeField]
    private AudioClip _spawnSound;
    [SerializeField]
    private AudioClip _receiveDamageSound;
    [SerializeField]
    private AudioClip _deathSound;

    private AudioSource _audioSource;
    private EntityHealth _entityHealth;

    void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _entityHealth = gameObject.GetComponent<EntityHealth>();

        if (_entityHealth != null)
        {
            _entityHealth.OnAfterEntityHit += PlayHitSound;
        }
        if(_deathSound != null &&
            TryGetComponent<EntityCorpse>(out EntityCorpse ec))
        {
            ec.DeathSound = _deathSound;
        }
    }

    private void PlayHitSound(object sender, int e)
    {
        PlaySound(_receiveDamageSound);
    }

    private void Start()
    {
        PlaySound(_spawnSound);
    }


    private void PlaySound(AudioClip sound)
    {
        if(sound != null)
        {
            _audioSource.clip = sound;
            _audioSource.Play();
        }
    }
}
