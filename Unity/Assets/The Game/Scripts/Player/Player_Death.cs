﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeathEffect
{
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void GetTouched()
    {
        //Play effect
        StartCoroutine(Die(Touched));
    }

    void GetCrushed()
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
        var delay = deathEffect.WaitDuration;
        //delay += Add the delay of the animation

        audioSource.clip = deathEffect.Sound;
        audioSource.Play();

        //Play animation

        yield return new WaitForSecondsRealtime(delay);
        Resetter.ResetLevel();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision == WorldBounds)
            FallOutOfBounds();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var layer = collision.gameObject.layer;
        bool isLayerOnLayerMask = DeathObjectMask == (DeathObjectMask | (1 << layer));

        if (isLayerOnLayerMask)
            GetTouched();
    }
}