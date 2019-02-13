using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Song
{
    public AudioClip Intro;
    public AudioClip Loop;
}

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySong(Song song)
    {
        if (song.Intro != null)
        {
            audioSource.clip = song.Intro;
        }

        else
        {
            audioSource.clip = song.Loop;
        }

        audioSource.Play();
    }
}
