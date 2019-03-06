using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    public static void ChangeMasterVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
    }
    public static void ChangeMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
    }
    public static void ChangeSFXVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", newVolume);
    }

    /*public static KeyCode InteractKey()
    {
        //return System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey"));
    }*/
}