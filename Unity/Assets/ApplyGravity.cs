using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionData
{
    public RaycastHit2D hitData;
}

public class ApplyGravity : MonoBehaviour
{
    public int rayCount = 8;

    Rigidbody2D rb;
    float velocityDown;


    Vector2 halfSize;

    [SerializeField]
    LayerMask collisionMask;

    [SerializeField]
    float skinWidth = 0.01f;

    public event System.Action<float, Transform> BeforeCollisionEnter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        halfSize = GetComponent<BoxCollider2D>().size / 2;
    }

    void MoveWithVelocity(float dt)
    {

        var translation = (Vector3)(-Physics2D.gravity.normalized) * velocityDown * dt;

        var hit = RaycastDown(translation.magnitude + skinWidth);

        if (hit != null)
        {
            var rayHit = hit.hitData;

            translation = (Vector3)rayHit.point - transform.position;
            translation.y += halfSize.y + skinWidth;
            translation.x = 0;

            BeforeCollisionEnter?.Invoke(velocityDown, rayHit.transform);

            velocityDown = 0;
        }

        transform.position += translation;
    }


    CollisionData RaycastDown(float distance)
    {
        var bottomleft = Vector3.zero;
        bottomleft.x -= halfSize.x;
        bottomleft.y -= halfSize.x;

        var bottomright = Vector3.zero;
        bottomright.x += halfSize.x;
        bottomright.y -= halfSize.y;
        var rayOrigin = transform.position + bottomleft;


        for (int i = 0; i < rayCount; i++)
        {
            var results = new RaycastHit2D[8];
            var hit = Physics2D.Raycast(rayOrigin, Physics2D.gravity.normalized, new ContactFilter2D() { useLayerMask = true, layerMask = collisionMask }, results, distance);

            Debug.DrawLine(rayOrigin, (Vector2)rayOrigin + Physics2D.gravity.normalized * distance, Color.yellow, 0.1f);

            if (results.Any(h => h.transform != null && h.transform != transform))
            {
                var hitdata = results.First(h => h.transform != transform);
                return new CollisionData() { hitData = hitdata };
            }

            rayOrigin += (bottomright - bottomleft) / rayCount;
        }

        return null;
    }

    void ApplyGravityToVelocity(float dt)
    {
        velocityDown += Physics2D.gravity.y * dt;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;

        ApplyGravityToVelocity(Time.deltaTime);
        MoveWithVelocity(Time.deltaTime);
    }
}