using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Enlarge : MonoBehaviour
{
    [SerializeField]
    float factor = 1.25f;

    TextMeshProUGUI t;

    private void Start()
    {
        t = GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Button>()?.onClick.AddListener(ReduceSize);
    }

    public void EnlargeSize()
    {
        t.fontSizeMax *= factor;
        t.fontSize *= factor;
    }

    public void ReduceSize()
    {
        t.fontSizeMax /= factor;
        t.fontSize /= factor;
    }
}