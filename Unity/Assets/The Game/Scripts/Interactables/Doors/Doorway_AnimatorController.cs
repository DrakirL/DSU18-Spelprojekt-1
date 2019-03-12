using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Default, First, Second, Third, Fourth, Fifth
}

public class Doorway_AnimatorController : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    DoorType Floor;

    bool isOpen;

    string StringFromType(string phrase)
    {
        string s = "";

        if (Floor != DoorType.Default)
            s = Floor + " " + "Floor "+ phrase;

        else
            s = phrase;

        return s;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void UpdateState(bool open, bool instant)
    {
        isOpen = open;
        animator.SetBool("isOpen", isOpen);
    }

    void Open()
    {
        animator.Play(StringFromType("Door Opening"), 1, 0);
    }

    void Close()
    {
        animator.Play(StringFromType("Door Closing"), 1, 0);
    }
}