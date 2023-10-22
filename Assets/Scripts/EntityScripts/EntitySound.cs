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
    private AudioClip _swingSound;
    [SerializeField]
    private AudioClip _deathSound;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
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
