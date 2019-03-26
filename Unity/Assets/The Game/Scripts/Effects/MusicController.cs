using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    AudioClip loopClip;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = SettingsManager.MusicVolume;
        Debug.Log("Music: " + SettingsManager.MusicVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying)
            return;

        source.clip = loopClip;
        source.loop = true;
        source.Play();
        Destroy(this);
    }
}
