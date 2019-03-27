using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource src;

    [SerializeField]
    private AudioClip Jump;
    [SerializeField]
    private AudioClip Restart;
    [SerializeField]
    private AudioClip Rotate;

    [SerializeField]
    private AudioClip Step;
    private Jump jump;
    private LevelResetter resetter;
    private WorldSpin spin;
    private Player_Walk walk;

    // Start is called before the first frame update
    private void Awake()
    {
        walk = GetComponent<Player_Walk>();
        src = GetComponent<AudioSource>();
        jump = GetComponent<Jump>();
        resetter = Camera.main.GetComponent<LevelResetter>();
        spin = GameObject.FindObjectOfType<WorldSpin>();

        resetter.BeforeLevelReset += () => { Play(Restart); };

        jump.HitGround += wasGrounded => { if(!wasGrounded) Play(Jump); };

        spin.BeforeWorldRotate += a => { Play(Rotate); };
    }

    private void Play(AudioClip c)
    {
        src.volume = SettingsManager.SFXVolume;
        src.clip = c;
        src.Play();
    }

    private void LateUpdate()
    {
        if (!jump.IsGrounded)
        {
            if (src.clip == Step)
                src.Stop();

            return;
        }
        
        if (walk.input == Vector2.zero)
        {
            if (src.clip == Step)
                src.Stop();

            return;
        }

        if (src.clip == Step && src.isPlaying)
            return;
        
        src.clip = Step;
        src.Play();
        

    }
}