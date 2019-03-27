using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    static SettingsManager()
    {
        ChangeScene.OnSceneChanged += () =>
        {
            OnMusicVolumeChanged = null;
            OnSFXVolumeChanged = null;
        };
    }

    public static Action OnMusicVolumeChanged;
    public static Action OnSFXVolumeChanged;

    public static float MasterVolume {
        get => PlayerPrefs.GetFloat("MasterVolume");
        set {
            PlayerPrefs.SetFloat("MasterVolume", value);
            OnMusicVolumeChanged?.Invoke();
            OnSFXVolumeChanged?.Invoke();
        }
    }

    public static float MusicVolume {
        get => PlayerPrefs.GetFloat("MusicVolume") * MasterVolume;
        set {
            PlayerPrefs.SetFloat("MusicVolume", value);
            OnMusicVolumeChanged?.Invoke();
        }
    }

    public static float SFXVolume {
        get => PlayerPrefs.GetFloat("SFXVolume") * MasterVolume;
        set {
            PlayerPrefs.SetFloat("SFXVolume", value);
            OnSFXVolumeChanged?.Invoke();
        }
    }

    /*public static KeyCode InteractKey()
    {
        //return System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey"));
    }*/
}