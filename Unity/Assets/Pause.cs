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
            togglePause();
        }
    }

    public void togglePause()
    {
        isPaused = Time.timeScale != 0;
        if (!isPaused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }

        UI.SetActive(isPaused);
    }
}
