using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EntityHealth))]
public class EntityCorpse : MonoBehaviour
{
    [SerializeField]
    private float _destroyTime = 2;
    [SerializeField]
    private bool _fadeOut = true;

    private EntityHealth _health;
    private SpriteRenderer _spriteRenderer;
    private RuntimeAnimatorController _animator;
    private AudioClip _deathSound;
    private float _deathSoundVolume;

    public RuntimeAnimatorController Animator { set => _animator = value; } // Animator needs to be injected by EntityAnimation
    public AudioClip DeathSound { set => _deathSound = value; } // DeathSound needs to be injected by EntitySound
    public float DeathSoundVolume { set => _deathSoundVolume = value; } // DeathSoundVolume needs to be injected by EntitySound

    private void Awake()
    {
        _health = GetComponent<EntityHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _health.OnEntityDeath += SpawnCorpse;
    }

    // When this entity dies, spawn a new gameobject at the same position with the same sprite and orientation, if fadeOut is true, set it's opacity to 0.7 and fade it to 0
    // If death animation or death sound are set, play them, then destroy the corpse after destroy time
    private void SpawnCorpse(object sender, HitData e)
    {
        GameObject corpse = new GameObject("Corpse");
        corpse.transform.position = gameObject.transform.position;

        SpriteRenderer rend = corpse.AddComponent<SpriteRenderer>();
        //rend.sprite = _spriteRenderer.sprite;
        rend.flipX = GetComponent<SpriteRenderer>().flipX;

        if(_fadeOut)
        {
            rend.color = new Color(1, 1, 1, 0.4f);
            rend.AddComponent<FadeSprite>().StartFade(1, 0.4f);
        }

        if (_animator != null)
        {
            Animator anim = corpse.AddComponent<Animator>();
            anim.runtimeAnimatorController = _animator;
            anim.Play("Death");
        }

        if(_deathSound != null)
        {
            AudioSource audio = corpse.AddComponent<AudioSource>();
            audio.clip = _deathSound;
            audio.volume = _deathSoundVolume;
            audio.Play();
        }
        Destroy(corpse, _destroyTime);
    }

    private void OnDestroy()
    {
        _health.OnEntityDeath -= SpawnCorpse;
    }
}
