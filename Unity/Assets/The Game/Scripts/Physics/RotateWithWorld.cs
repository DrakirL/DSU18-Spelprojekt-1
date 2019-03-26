using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithWorld : MonoBehaviour
{
    float height;
    Vector3 offset;
    
    void Awake()
    {
        var coll = GetComponent<Collider2D>();
        if (coll != null)
        {
            height = coll.bounds.extents.y + 0.1f;
            offset = coll.offset;
        }

        var world = GameObject.Find("World").GetComponent<WorldSpin>();

        world.BeforeWorldRotate += BeforeWorldRotate;
        world.OnWorldRotateBy += OnWorldRotate;
    }

    void BeforeWorldRotate(float by)
    {
        Debug.Log(by);

        if (Mathf.Abs(by) > 90)
            return;

        var origin = transform.position + offset;
        var dir = (by < 0) ? Vector3.right : Vector3.left;

        var hit = Physics2D.Raycast(origin, dir, height, LayerMask.GetMask("Platform"));
        Debug.DrawLine(origin, origin + dir * height, Color.yellow, 5);

        if (!hit)
            return;

        var point = hit.point;
        var newOrigin = (Vector3)point - dir * height;

        transform.position = newOrigin;

        Debug.Log("point: " + point);
    }

    private void OnWorldRotate(float by)
    {
        transform.Rotate(0, 0, by);
    }
}