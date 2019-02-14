using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class ShakeCamera : MonoBehaviour
{
    [SerializeField]
    float magnitude;

    [SerializeField]
    float fadeoutTime;

    [SerializeField]
    float fadeinTime;

    [SerializeField]
    float roughness;

    // Start is called before the first frame 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeinTime, fadeoutTime); 
        }
    }
}
