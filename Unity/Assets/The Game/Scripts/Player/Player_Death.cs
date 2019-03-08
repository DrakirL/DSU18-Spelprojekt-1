using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CauseOfDeath
{
    Touched, Crushed, ForceReset
}

[System.Serializable]
public class DeathEffect
{
    public CauseOfDeath Cause;
    public AudioClip Sound;
    public float WaitDuration;
}

public class Player_Death : MonoBehaviour
{
    bool isDead;

    public LevelResetter Resetter;
    public Collider2D WorldBounds;
    public LayerMask DeathObjectMask;

    public DeathEffect Touched;
    public DeathEffect Crushed;
    public DeathEffect OutOfBounds;
    public DeathEffect ForcedReset;

    AudioSource audioSource;
    LevelResetter resetter;

    public event System.Action<CauseOfDeath> BeforeDie;
    public event System.Action AfterDie;

    private void Awake()
    {
        OmniDisabler.SetActiveBasedOnEnable(this);

        audioSource = GetComponent<AudioSource>();

        resetter = Camera.main.GetComponent<LevelResetter>();
        resetter.AfterResetLevel += t => Respawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isDead)
            Die(ForcedReset);
    }

    void GetTouched()
    {
        if (!isDead)
            Die(Touched);
    }

    public void GetCrushed()
    {
        if (!isDead)
            Die(Crushed);
    }

    void FallOutOfBounds()
    {
        if (!isDead)
            Die(OutOfBounds);
    }
    void Die(DeathEffect deathEffect)
    {
        StartCoroutine(die());
        IEnumerator die()
        {
            isDead = true;

            var col = GetComponent<Collider2D>();
            var rb2d = GetComponent<Rigidbody2D>();

            BeforeDie?.Invoke(deathEffect.Cause);
            col.enabled = false;
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;

            audioSource.PlayOneShot(deathEffect.Sound);
            var delay = deathEffect.WaitDuration;// + deathEffect?.Sound.length ?? 0;

            yield return new WaitForSecondsRealtime(delay);
            Resetter.StartResetLevel();
        }
    }

    void Respawn()
    {
        isDead = false;

        var col = GetComponent<Collider2D>();
        var rb2d = GetComponent<Rigidbody2D>();

        col.enabled = true;
        rb2d.gravityScale = 1;
        AfterDie?.Invoke();
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == WorldBounds)
            FallOutOfBounds();
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryDeath(collision.gameObject.layer);
    private void OnTriggerEnter2D(Collider2D collision) => TryDeath(collision.gameObject.layer);

    void TryDeath(int layer)
    {
        bool isLayerOnLayerMask = DeathObjectMask == (DeathObjectMask | (1 << layer));

        if (isLayerOnLayerMask)
            if (!isDead)
                GetTouched();
    }
}