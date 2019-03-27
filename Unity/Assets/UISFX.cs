using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour
{
    AudioSource src;
    [SerializeField]
    AudioClip hover;
    [SerializeField]
    AudioClip press;


    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.volume = SettingsManager.SFXVolume;
    }


    public void PlayHover()
    {
        if (src.isPlaying)
            return;
        src.volume = SettingsManager.SFXVolume;
        src.clip = hover;
        src.Play();
    }

    public void PlayPress()
    {
        src.volume = SettingsManager.SFXVolume;
        src.clip = press;
        src.Play();
    }
}
