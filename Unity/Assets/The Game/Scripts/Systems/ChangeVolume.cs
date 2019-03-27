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
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            UpdateMaster();
            UpdateMusic();
            UpdateSFX();
        }
            
        

        MusicVolumeSlider.value = SettingsManager.MusicVolume / SettingsManager.MasterVolume;
        SFXVolumeSlider.value = SettingsManager.SFXVolume / SettingsManager.MasterVolume;
        MasterVolumeSlider.value = SettingsManager.MasterVolume;
    }

    public void UpdateMaster() => SettingsManager.MasterVolume = MasterVolumeSlider.value;
    public void UpdateMusic() => SettingsManager.MusicVolume = MusicVolumeSlider.value;
    public void UpdateSFX() => SettingsManager.SFXVolume = SFXVolumeSlider.value;
    
    

   
}