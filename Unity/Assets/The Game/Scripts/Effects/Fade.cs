using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    bool isFadingIn;
    bool isFadingOut;

    public float FadeDuration;
    float fadeDurationPassed;

    float startAlpha;
    float endAlpha;

    void Awake()
    {
        FindRenderers(this.transform);
        
        DoorwayTransitions.OnEnteredDoor += OnLevelEnter;
    }

    void FindRenderers(Transform origin)
    {
        SpriteRenderer s = origin.GetComponent<SpriteRenderer>();

        if (s != null)
            renderers.Add(s);

        if (origin.childCount > 0)
        {
            for (int i = 0; i < origin.childCount; i++)
                FindRenderers(origin.GetChild(i));
        }
    }

    private void OnLevelEnter()
    {
        if (transform == DoorwayTransitions.NextRoom)
            FadeIn();

        else if (transform == DoorwayTransitions.CurrentRoom)
            FadeOut();
    }

    void Update()
    {
        if (isFadingIn || isFadingOut)
        {
            if (fadeDurationPassed < FadeDuration)
            {
                fadeDurationPassed += Time.unscaledDeltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, fadeDurationPassed / FadeDuration);

                if (isFadingIn ^ isFadingOut)
                    ChangeAlphas(newAlpha);

                if (fadeDurationPassed >= FadeDuration)
                {
                    isFadingIn = false;
                    isFadingOut = false;
                }
            }
        }
    }

    void ChangeAlphas(float newAlpha)
    {
        for(int i = 0; i < renderers.Count; i++)
            renderers[i].color = new Color(renderers[i].color.r, renderers[i].color.g, renderers[i].color.b, newAlpha);
    }
    
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