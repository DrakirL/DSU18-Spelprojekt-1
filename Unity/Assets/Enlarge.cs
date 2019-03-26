using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Enlarge : MonoBehaviour
{
    [SerializeField]
    float factor = 1.25f;

    RectTransform t;

    private void Start()
    {
        t = GetComponent<RectTransform>();
        GetComponent<Button>()?.onClick.AddListener(() =>
        {
            ReduceSize();
        });
    }

    public void EnlargeSize()
    {
        t.sizeDelta *= factor;
    }

    public void ReduceSize() => t.sizeDelta /= factor;
}