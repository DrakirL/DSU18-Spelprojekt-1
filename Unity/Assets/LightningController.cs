using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    Bounds bounds;

    [SerializeField]
    float spawnRate = 0.5f;

    [SerializeField]
    GameObject[] lightnings;

    float sinceLastSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        var mask = transform.GetChild(0);
        mask.parent = null;
        mask.localScale = transform.localScale;
        mask.parent = transform;

        mask.gameObject.SetActive(true);

        bounds = GetComponent<Collider2D>().bounds;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 1)
            return;

        sinceLastSpawn += Time.deltaTime;
        if (sinceLastSpawn < spawnRate)
            return;

        sinceLastSpawn = 0;

        var randomLightning = lightnings[Random.Range(0, lightnings.Length)];
        var lightning = Instantiate(randomLightning);
        var lt = lightning.transform;

        var extents = bounds.extents;

        lt.parent = transform;
        lt.localPosition = new Vector3()
        {
            x = Random.Range(-extents.x, extents.x),
            y = Random.Range(-extents.y, extents.y)
        };

        lt.Rotate(0, 0, Random.Range(0f, 360f));

        lt.parent = null;
        lt.localScale = Vector3.one;
        lt.parent = transform;

    }
}
