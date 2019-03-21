using UnityEngine;
using UnityEngine.UI;

public class UINote : MonoBehaviour
{
    [SerializeField]
    private int noteCount;
    private float startAlpha;



    private Image img;
    private int foundNotes;

    private void Start()
    {
        img = GetComponent<Image>();
        startAlpha = img.color.a;
        var res = GameObject.FindObjectOfType<LevelResetter>();
        res.AfterResetLevel += door => ResetNotesIn(door.Room);




    }

    //Todo maybe just add script to notes?
    private void ResetNotesIn(Transform obj)
    {
        if (obj.tag == "Notes")
        {
            foundNotes -= 2;
            FillPartial();

        }

        foreach (Transform child in obj)
            ResetNotesIn(child);
    }

    public void FillPartial()
    {
        foundNotes++;
        var c = img.color;
        c.a = startAlpha + (1 - startAlpha) * (((float)foundNotes) / noteCount);
        img.color = c;

        Debug.Log("Found a note");
    }

    public void Fill()
    {
        var c = img.color;
        c.a = 1;
        img.color = c;

    }

    public void Unfill()
    {
        var c = img.color;
        c.a = startAlpha;
        img.color = c;
    }
}
