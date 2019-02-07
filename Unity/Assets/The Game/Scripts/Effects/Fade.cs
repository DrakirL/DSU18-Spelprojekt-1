using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    bool isFadingIn;
    bool isFadingOut;

    public float FadeDuration;
    float fadeDurationPassed;

    float startAlpha;
    float endAlpha;

    CameraMove move;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        var color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;

        move = Camera.main.GetComponent<CameraMove>();
        move.OnLevelEnter += OnLevelEnter;
    }

    private void OnLevelEnter(Transform newLevel)
    {
        if (transform.parent == newLevel)
            FadeOut();
        else if (transform.parent == move.currentRoom)
            FadeIn();
    }

    void Update()
    {
        if (isFadingIn || isFadingOut)
        {
            if (fadeDurationPassed < FadeDuration)
            {
                fadeDurationPassed += Time.unscaledDeltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, fadeDurationPassed / FadeDuration);

                if (isFadingOut)
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

                else if (isFadingIn)
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

                if (fadeDurationPassed >= FadeDuration)
                {
                    isFadingIn = false;
                    isFadingOut = false;
                }
            }
        }
    }

// Start is called before the first frame update
    public void FadeIn()
    {
        startAlpha = 0;
        endAlpha = 1;

        fadeDurationPassed = 0;
        isFadingIn = true;
    }

    // Update is called once per frame
    public void FadeOut()
    {
        startAlpha = 1;
        endAlpha = 0;

        fadeDurationPassed = 0;
        isFadingOut = true;
    }
}