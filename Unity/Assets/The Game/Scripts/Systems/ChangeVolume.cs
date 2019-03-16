using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    void Start()
    {
        ResetSliders();
        PrepareSliders();
    }

    void PrepareSliders()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    void ResetSliders()
    {
        MasterVolumeSlider.value = MasterVolumeSlider.maxValue;
        MusicVolumeSlider.value = MusicVolumeSlider.maxValue;
        SFXVolumeSlider.value = SFXVolumeSlider.maxValue;

        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void ChangeMasterVolume()
    {
        SettingsManager.ChangeMasterVolume(MasterVolumeSlider.value);
    }
    public void ChangeMusicVolume()
    {
        SettingsManager.ChangeMusicVolume(MusicVolumeSlider.value);
    }
    public void ChangeSFXVolume()
    {
        SettingsManager.ChangeSFXVolume(SFXVolumeSlider.value);
    }
}