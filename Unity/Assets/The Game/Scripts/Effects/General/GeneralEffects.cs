using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffects : MonoBehaviour
{
    [SerializeField]
    AudioClip audioClip;

    public void PlaySound()
    {
        var scr = GetComponent<AudioSource>();

        scr.volume = SettingsManager.SFXVolume;
        scr.clip = audioClip;
        scr.Play();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}