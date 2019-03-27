using UnityEngine;

public class GeneralEffects : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private void Start()
    {
        var src = GetComponent<AudioSource>();
        if (src != null)
            src.volume = SettingsManager.SFXVolume;
    }

    public void PlaySound()
    {
        var src = GetComponent<AudioSource>();

        src.volume = SettingsManager.SFXVolume;
        if (audioClip != null)
            src.clip = audioClip;
        src.Play();
    }

    public void DestroySelf() => Destroy(gameObject);
}