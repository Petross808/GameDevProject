using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySound : MonoBehaviour
{
    [SerializeField]
    private AudioClip _spawnSound;
    [SerializeField]
    [Range(0f, 1f)]
    private float _spawnVolume;

    [SerializeField]
    private AudioClip _receiveDamageSound;
    [SerializeField]
    [Range(0f, 1f)]
    private float _receiveDamageVolume;

    [SerializeField]
    private AudioClip _deathSound;
    [SerializeField]
    [Range(0f, 1f)]
    private float _deathVolume;

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
            ec.DeathSoundVolume = _deathVolume;
        }
    }

    private void PlayHitSound(object sender, int e)
    {
        _audioSource.volume = _receiveDamageVolume;
        PlaySound(_receiveDamageSound);
    }

    private void Start()
    {
        _audioSource.volume = _spawnVolume;
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
