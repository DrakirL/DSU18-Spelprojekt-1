using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    public static void ChangeMasterVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
    }
    public static float MasterVolume { get => PlayerPrefs.GetFloat("MasterVolume"); }

    public static void ChangeMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
    }
    public static float MusicVolume { get => PlayerPrefs.GetFloat("MusicVolume") * MasterVolume; }

    public static void ChangeSFXVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", newVolume);
    }
    public static float SFXVolume { get => PlayerPrefs.GetFloat("SFXVolume") * MasterVolume; }

    /*public static KeyCode InteractKey()
    {
        //return System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey"));
    }*/
}