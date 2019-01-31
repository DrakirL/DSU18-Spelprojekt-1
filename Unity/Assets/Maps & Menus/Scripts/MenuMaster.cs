using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioRelated
{
    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
}

public class MenuMaster : MonoBehaviour
{
    //Managing the menues in general
    public SettingsManager settingsManager;
    void Start()
    {
        PrepareSliders();
    }
    public void ResetOptions()
    {
        ResetSliders();
    }

    public GameObject[] MenuLayers;
    public void ChangeLayers(int newLayer)
    {
        for (int i = 0; i < MenuLayers.Length; i++)
        {
            if (i == newLayer)
            {
                MenuLayers[i].SetActive(true);
            }

            else
            {
                MenuLayers[i].SetActive(false);
            }
        }
    }

    //Playing and Quitting
    public void PlayGame()
    {
        Debug.Log("Temporary message saying the game is being played");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Temporary message saying that you've quit the game");
    }

    //Audio Settings
    public AudioRelated audioRelated;

    void PrepareSliders()
    {
        audioRelated.MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        audioRelated.MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        audioRelated.SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    void ResetSliders()
    {
        audioRelated.MasterVolumeSlider.value = audioRelated.MasterVolumeSlider.maxValue;
        audioRelated.MusicVolumeSlider.value = audioRelated.MusicVolumeSlider.maxValue;
        audioRelated.SFXVolumeSlider.value = audioRelated.SFXVolumeSlider.maxValue;

        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void ChangeMasterVolume()
    {
        settingsManager.ChangeMasterVolume(audioRelated.MasterVolumeSlider.value);
    }
    public void ChangeMusicVolume()
    {
        settingsManager.ChangeMusicVolume(audioRelated.MusicVolumeSlider.value);
    }
    public void ChangeSFXVolume()
    {
        settingsManager.ChangeSFXVolume(audioRelated.SFXVolumeSlider.value);
    }
}