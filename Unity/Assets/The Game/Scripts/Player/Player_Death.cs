﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CauseOfDeath
{
    Touched, Crushed, OutOfBounds
}

[System.Serializable]
public class DeathEffect
{
    public CauseOfDeath Cause;
    public AudioClip Sound;
    //public Animation Anim;
    public float WaitDuration;
}

public class Player_Death : MonoBehaviour
{
    public LevelResetter Resetter;
    public Collider2D WorldBounds;
    public LayerMask DeathObjectMask;

    public DeathEffect Touched;
    public DeathEffect Crushed;
    public DeathEffect OutOfBounds;

    AudioSource audioSource;
    LevelResetter resetter;

    public event System.Action<CauseOfDeath> BeforeDie;
    public event System.Action AfterDie;

    private void Awake()
    {
        resetter = Camera.main.GetComponent<LevelResetter>();
        resetter.AfterResetLevel += t => Respawn();
        audioSource = GetComponent<AudioSource>();
    }
    
    void GetTouched()
    {
        //Play effect
        StartCoroutine(Die(Touched));
    }

    public void GetCrushed()
    {
        //Play effect
        StartCoroutine(Die(Crushed));
    }

    void FallOutOfBounds()
    {
        //Play effect
        StartCoroutine(Die(OutOfBounds));
    }

    IEnumerator Die(DeathEffect deathEffect)
    {
        var col = GetComponent<Collider2D>();
        var rb2d = GetComponent<Rigidbody2D>();

        BeforeDie?.Invoke(deathEffect.Cause);
        col.enabled = false;
        rb2d.gravityScale = 0;

        var delay = deathEffect.WaitDuration;
        //delay += Add the delay of the animation

        audioSource.clip = deathEffect.Sound;
        audioSource.Play();

        //Play animation

        yield return new WaitForSecondsRealtime(delay);
        Resetter.StartResetLevel();
    }

    void Respawn()
    {
        var col = GetComponent<Collider2D>();
        var rb2d = GetComponent<Rigidbody2D>(); 

        col.enabled = true;
        rb2d.gravityScale = 1;
        AfterDie?.Invoke();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == WorldBounds)
            FallOutOfBounds();
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryDeath(collision.gameObject.layer);
    private void OnTriggerEnter2D(Collider2D collision) => TryDeath(collision.gameObject.layer);


    void TryDeath(int layer)
    {
        bool isLayerOnLayerMask = DeathObjectMask == (DeathObjectMask | (1 << layer));

        if (isLayerOnLayerMask)
            GetTouched();
    }



}