using UnityEngine;

public class LerpBlink : MonoBehaviour
{
    private float lerpTaken;

    [SerializeField]
    private float lerpDuration;

    [SerializeField]
    private Color To;
    private Color from;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        from = sr.color;
    }

    // Update is called once per frame
    private void Update()
    {
        float t = lerpTaken / lerpDuration;

        if (t >= 1)
        {
            t = 1 - (t - 1);
            if (t <= 0)
                lerpTaken = 0;
        }


        sr.color = Color.Lerp(from, To, t);
        lerpTaken += Time.deltaTime;
    }

}
