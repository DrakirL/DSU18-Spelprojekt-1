using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField]
    AudioSource src;

    [SerializeField]
    float JumpVolume;

    [SerializeField]
    float RestartVolume;

    [SerializeField]
    AudioClip Jump;
    [SerializeField]
    AudioClip Restart;

    [SerializeField]
    Jump jump;

    [SerializeField]
    LevelResetter resetter;

    // Start is called before the first frame update
    void Start()
    {
        resetter.BeforeLevelReset += () => {
            src.volume = RestartVolume;
            src.clip = Restart;
            src.Play();
        };
        jump.HitGround +=  () => {
            src.volume = JumpVolume;
            src.clip = Jump;
            src.Play();
        };

    }
}
