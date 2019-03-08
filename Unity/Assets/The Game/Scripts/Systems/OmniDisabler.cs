﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class OmniDisabler
{
    public static event Action OnEnable;
    public static event Action OnDisable;

    public static bool IsEnabled { get; private set; }

    public static void Enable()
    {
        IsEnabled = true;
        Time.timeScale = 1;
        OnEnable?.Invoke();
    }

    public static void Disable()
    {
        IsEnabled = false;
        Time.timeScale = 0;
        OnDisable?.Invoke();
    }

    public static void SetActiveBasedOnEnable(Behaviour behaviour)
    {
        OnEnable += () => behaviour.enabled = true;
        OnDisable += () => behaviour.enabled = false;
    }
}