using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    KeyCode pauseButton = KeyCode.P;
    [SerializeField]
    GameObject UI;
    bool isPaused;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (isPaused)
                togglePause();
            else if (Time.deltaTime != 0)
                togglePause();
        }
    }

    public void togglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            OmniDisabler.Disable();

        else
            OmniDisabler.Enable();

        UI.SetActive(isPaused);
    }
}
