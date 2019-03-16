using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    AudioSource src;

    [SerializeField]
    AudioClip Jump;
    [SerializeField]
    AudioClip Restart;
    [SerializeField]
    AudioClip Rotate;

    Jump jump;
    LevelResetter resetter;
    WorldSpin spin;

    // Start is called before the first frame update
    void Awake()
    {
        src = GetComponent<AudioSource>();
        jump = GetComponent<Jump>();
        resetter = Camera.main.GetComponent<LevelResetter>();
        spin = GameObject.FindObjectOfType<WorldSpin>();

        resetter.BeforeLevelReset += () => { Play(Restart); };

        jump.HitGround +=  () => { Play(Jump); };

        spin.BeforeWorldRotate += () => { Play(Rotate); };
    }

    void Play(AudioClip c)
    {
        src.volume = SettingsManager.SFXVolume;
        src.clip = c;
        src.Play();
    }
}