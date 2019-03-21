using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private List<SpriteRenderer> renderers;
    private bool isFadingIn;
    private bool isFadingOut;

    private float FadeDuration;
    private float fadeDurationPassed;
    private float startAlpha;
    private float endAlpha;

    private void Awake()
    {
        DoorwayTransitions.OnEnteredDoor += OnLevelEnter;
        FadeDuration = GameObject.FindObjectOfType<CameraMove>().MoveDuration / 2;
    }

    private void FindRenderers(Transform origin)
    {
        SpriteRenderer s = origin.GetComponent<SpriteRenderer>();

        if (s != null)
            renderers.Add(s);

        foreach (Transform child in origin)
            FindRenderers(child);

    }

    private void OnLevelEnter()
    {
        if (transform == DoorwayTransitions.NextRoom)
        {
            UpdateRenderers();
            FadeIn();
        }
        else if (transform == DoorwayTransitions.CurrentRoom)
        {
            UpdateRenderers();
            FadeOut();
        }
    }

    private void UpdateRenderers()
    {
        if (renderers != null)
            return;

        renderers = new List<SpriteRenderer>();
        FindRenderers(this.transform);

    }

    private void Update()
    {
        if (isFadingIn || isFadingOut)
        {

            fadeDurationPassed += Time.unscaledDeltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, fadeDurationPassed / FadeDuration);
            //Debug.Log(transform.name + " is fading: new alpha -> " + newAlpha);


            //xor for first room case?
            if (isFadingOut ^ isFadingOut)
            {
                Debug.Log("Changed alppha");
                ChangeAlphas(newAlpha);
            }
            Debug.Log(fadeDurationPassed);
            if (fadeDurationPassed >= FadeDuration)
            {
                isFadingIn = false;
                isFadingOut = false;
                //Debug.Log(transform.name + " is done fading");
            }

        }
    }

    private void ChangeAlphas(float newAlpha)
    {
        foreach (var item in renderers)
        {
            var c = item.color;
            c.a = newAlpha;
            item.color = c;
            //Debug.Log(item.name);
        }

    }

    public void FadeIn()
    {
        startAlpha = 0;
        endAlpha = 1;

        fadeDurationPassed = 0;
        isFadingIn = true;
        isFadingOut = false;
    }

    public void FadeOut()
    {
        startAlpha = 1;
        endAlpha = 0;

        fadeDurationPassed = 0;
        isFadingIn = false;
        isFadingOut = true;
    }
}