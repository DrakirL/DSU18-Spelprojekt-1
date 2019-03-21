using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncMusic : MonoBehaviour
{

    [SerializeField]
    bool reset;

    [SerializeField]
    bool destroyAfterStart;

    [SerializeField]
    string musicIdentifier = "musicID";

    AudioSource src;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        Debug.Log(PlayerPrefs.GetFloat(musicIdentifier));
        src.time = PlayerPrefs.GetFloat(musicIdentifier);
        
    }

    private void Update()
    {
        t = src.time;
    }



    private void OnDestroy()
    {
        Debug.Log("D: " + t);
        PlayerPrefs.SetFloat(musicIdentifier,t);
    }
}
