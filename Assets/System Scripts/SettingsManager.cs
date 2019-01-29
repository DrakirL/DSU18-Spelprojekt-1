using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    public void ChangeMasterVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
    }
    public void ChangeMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
    }
    public void ChangeSFXVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", newVolume);
    }
}