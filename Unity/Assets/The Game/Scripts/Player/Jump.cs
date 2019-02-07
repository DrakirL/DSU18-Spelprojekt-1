using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float JumpMaxHeight;
    private Rigidbody2D rb;

    [SerializeField]
    [Range(0, 10)]
    private float FallModifier = 0.7f;

    [SerializeField]
    [Range(0,1)]
    float LowJumpModifier = 0.5f;

    private float colliderHeight;

    [SerializeField]
    [Range(0, 0.1f)]
    float velocitySensitivity;

    Vector3 colliderOffset;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var col = GetComponentInChildren<BoxCollider2D>();
        colliderHeight = col.bounds.extents.y;
        colliderOffset = col.offset;

    }


    [SerializeField]
    [Range(0f,0.1f)]
    float skinWidth;

    private bool isJumping = false;

        bool isGrounded = false;
    // Update is called once per frame
    private void Update()
    {
        var hit = Physics2D.Raycast(
            origin: transform.position + colliderOffset, 
            direction: Physics2D.gravity.normalized,
            distance:  colliderHeight + skinWidth,
            layerMask: LayerMask.GetMask("Platform")
        );

        isGrounded = hit;

        if (isJumping && isGrounded)
            isJumping = false;


        if (CanJump())
        {
            JumpToHeight();
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            var vel = rb.velocity;
            vel += -Physics2D.gravity * (1 - LowJumpModifier) * Time.deltaTime;
            rb.velocity = vel;
        }


    }


    bool CanJump()
    {
        bool velocityIsLowEnough = false;

        if (Physics2D.gravity.x != 0)
            velocityIsLowEnough = rb.velocity.x <= velocitySensitivity;
        else
            velocityIsLowEnough = rb.velocity.y <= velocitySensitivity;

        return Input.GetKey(KeyCode.Space) && isGrounded && !isJumping && velocityIsLowEnough;
    }

    private void JumpToHeight()
    {
        var vel = rb.velocity;
        
        var jumpVel = -Physics2D.gravity.normalized * Mathf.Sqrt(Mathf.Abs(2 * Physics2D.gravity.magnitude * LowJumpModifier * JumpMaxHeight));
        vel += jumpVel;
        rb.velocity = vel;

    }
}
