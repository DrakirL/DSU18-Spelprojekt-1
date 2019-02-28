using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateChildren : MonoBehaviour
{
    public float speed =  10f;
    [SerializeField]
    Transform[] ignores;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform t in transform)
        {
            bool b = false;
            foreach (var ti in ignores)
            {
                if (t == ti)
                {
                    b = true;
                    break;
                }
            }

            if (b)
                continue;

            t.Rotate(0, 0, speed * Time.deltaTime);
        }
        
    }
}
