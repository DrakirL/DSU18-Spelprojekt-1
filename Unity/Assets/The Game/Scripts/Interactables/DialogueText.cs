using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueText : MonoBehaviour
{
    [SerializeField]
    string[] dialogue;

    TextMeshPro tbx;
    TextMeshPro Tbx
    {
        get
        {
            if (tbx == null)
                tbx = GetComponent<TextMeshPro>();

            return tbx;
        }
    }

    GameObject player;
    GameObject Player
    {
        get
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            return player;
        }

    }

    Player_Walk Walk => Player?.GetComponent<Player_Walk>() ?? null;
    Jump Jump => Player?.GetComponent<Jump>() ?? null;
    Player_AnimatorController Anim => Player?.GetComponent<Player_AnimatorController>() ?? null;
    Interactor Inter => Player?.GetComponent<Interactor>() ?? null;

    WorldSpin spin;
    WorldSpin Spin
    {
        get
        {
            if (spin == null)
                spin = GameObject.FindObjectOfType<WorldSpin>();

            return spin;
        }
    }

    int currentText;

    [SerializeField]
    KeyCode key = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKey(key))
            Close();
    }

    void Display()
    {
        Tbx.text = dialogue[currentText];

        if (currentText == 0)
        {
            Tbx.transform.parent.gameObject.SetActive(true);
            Anim.PlayIdle();
            OmniDisabler.DisableNormalTimescale();
            Inter.enabled = true;
        }
    }

    public void Close()
    {
        currentText = 0;
        Tbx.transform.parent.gameObject.SetActive(false);
        OmniDisabler.Enable();
    }

    public void MoveNext()
    {
        if (currentText >= dialogue.Length)
        {
            Close();
            return;
        }

        Display();
        currentText++;
        
    }
}