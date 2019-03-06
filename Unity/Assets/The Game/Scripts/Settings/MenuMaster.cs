using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMaster : MonoBehaviour
{
    void Start()
    {
        ChangeLayers(0);
    }

    public GameObject[] MenuLayers;
    public void ChangeLayers(int newLayer)
    {
        for (int i = 0; i < MenuLayers.Length; i++)
        {
            if (i == newLayer)
            {
                MenuLayers[i].SetActive(true);
            }

            else
            {
                MenuLayers[i].SetActive(false);
            }
        }
    }
}