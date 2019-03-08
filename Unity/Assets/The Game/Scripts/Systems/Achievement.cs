using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    int deathCount;
    int flipCount;
    float playStartTime;

    int objectsFound;

    public void IncreaseDeathCount() => deathCount++;
    public void IncreaseFlipCount() => flipCount++;
    public void IncreaseObjectsFound() => objectsFound++;

    private void Awake()
    {
        var death = GameObject.Find("Player").GetComponent<Player_Death>();
        

        death.AfterDie += IncreaseDeathCount;
    }

    public void Reset()
    {
        deathCount = flipCount = objectsFound = 0;
        playStartTime = Time.time;
    }
}