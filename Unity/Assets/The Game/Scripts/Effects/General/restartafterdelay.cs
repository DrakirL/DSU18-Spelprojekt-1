using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartafterdelay : MonoBehaviour
{
    public float delay;
    private float timePassed;

    public string animationToPlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= delay)
        {
            Animator anim = GetComponent<Animator>();
            anim.Play(animationToPlay, 0, 0);
            timePassed = 0;
        }
    }
}
