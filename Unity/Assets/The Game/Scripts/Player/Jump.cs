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

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    private bool isJumping = false;

    // Update is called once per frame
    private void Update()
    {
        bool isGrounded = false;
        var hit = Physics2D.Raycast(
            origin: transform.position, 
            direction: Physics2D.gravity.normalized,
            distance:  colliderHeight + 0.1f,
            layerMask: LayerMask.GetMask("Platform")
        );


        isGrounded = hit;

        if (isJumping && isGrounded)
            isJumping = false;
        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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

    private void JumpToHeight()
    {
        var vel = rb.velocity;
        
        var jumpVel = -Physics2D.gravity.normalized * Mathf.Sqrt(Mathf.Abs(2 * Physics2D.gravity.magnitude * LowJumpModifier * JumpMaxHeight));

        vel += jumpVel;
        rb.velocity = vel;

    }
}
