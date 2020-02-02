using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ItemLifespanController : MonoBehaviour
{
    public ParticleSystem boomOnDestroy;
    public ParticleSystem boomOnDestroy2;
    public int lifeSpan = 30;


    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("WarnOfExplosion",Random.Range(15,25));
        
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void WarnOfExplosion()
    {
        boomOnDestroy.Play();
        boomOnDestroy2.Play();
        Invoke("ExplodeItem",4);
    }

    void ExplodeItem()
    {

        audioManager.PlaySoundEffect(SoundEffectType.Explosion);
        Destroy(this.gameObject);
    }

}
