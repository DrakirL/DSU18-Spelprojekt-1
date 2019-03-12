﻿using UnityEngine;

public class Unlocker : MonoBehaviour
{
    public InteractableLock[] Locks;
    private bool active;

    private LevelResetter resetter;

    private void Awake()
    {
        resetter = GameObject.FindObjectOfType<LevelResetter>();
        resetter.AfterResetLevel += t =>
        {
            var v = 1;
            if (!active)
                v = 0;

            foreach (var l in Locks)
            {
                if(l != null)
                    l.PrerequisitesFulfilled -= v;
            }

            active = false;
            ResetAnim();
            
        };
        ResetAnim();
    }

    private void OnEnable()
    {
        ResetAnim();
    }

    public void togglePrerequisite()
    {
        active = !active;
        var v = 1;
        if (!active)
            v = -1;

        foreach (var l in Locks)
        {
            if(l != null)
                l.PrerequisitesFulfilled += v;
        }
    }

    void ResetAnim()
    {
        var anim = GetComponent<Animator>();

        if (active)
            anim.Play("PullLeverDown", 0, 1);

        if (!active)
            anim.Play("PullLeverUp", 0, 1);
    }
}