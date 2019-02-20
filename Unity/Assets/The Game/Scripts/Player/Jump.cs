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
    [Range(0, 1)]
    private float LowJumpModifier = 0.5f;

    private float colliderHeight;

    [SerializeField]
    float hardLandingVel = 2;

    float hitGroundVel;

    bool hardLanding;
    public bool HardLanding => -hardLandingVel >= hitGroundVel;



    [SerializeField]
    [Range(0, 0.1f)]
    float velocitySensitivity;

    Vector3 colliderOffset;

    bool isEnabled = true;
    private void Awake()
    {
        var death = GetComponent<Player_Death>();
        death.BeforeDie += Disable;
        death.AfterDie += Reenable;
    }

    void Disable()
    {
        isEnabled = false;
    }

    void Reenable()
    {
        isEnabled = true;
    }

    private float colWidth;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var col = GetComponentInChildren<BoxCollider2D>();
        colliderHeight = col.bounds.extents.y;
        colliderOffset = col.offset;
        colWidth = col.bounds.extents.x * 2;
    }

    [SerializeField]
    LayerMask GroundMask;

    [SerializeField]
    private int HorizontalRaycastCount;

    [SerializeField]
    [Range(0f, 0.1f)]
    private float skinWidth;

    [SerializeField]
    public bool isJumping = false;
    [SerializeField]
    public bool isGrounded = false;

    public event System.Action HitGround;

    // Update is called once per frame
    private void Update()
    {
        var origin = transform.position + colliderOffset;
        origin.x += skinWidth - colWidth / 2f;

        for (int i = 0; i < HorizontalRaycastCount; i++)
        {
            var hit = Physics2D.Raycast(
                origin: origin,
                direction: Physics2D.gravity.normalized,
                distance: colliderHeight + skinWidth,
                layerMask: GroundMask
            );

            Debug.DrawLine(origin, (Vector2)origin + Physics2D.gravity.normalized * (colliderHeight + skinWidth)) ;
            origin += Vector3.right * ((colWidth - (2*skinWidth)) / (HorizontalRaycastCount - 1));
            //origin.x -= ((2 * skinWidth)/HorizontalRaycastCount);

            isGrounded = hit;

            if (isGrounded)
            {
                Debug.Log("IsGRounded");
                HitGround();
                break;
            }
        }

        if (isJumping && isGrounded)
            isJumping = false;

        if (CanJump())
        {
            JumpToHeight();
            isJumping = true;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            var vel = rb.velocity;
            vel += -Physics2D.gravity * (1 - LowJumpModifier) * Time.deltaTime;
            rb.velocity = vel;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitGroundVel = -collision.relativeVelocity.y;
    }

    private bool CanJump()
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
        if (!isEnabled)
            return;

        var vel = rb.velocity;

        var jumpVel = -Physics2D.gravity.normalized * Mathf.Sqrt(Mathf.Abs(2 * Physics2D.gravity.magnitude * LowJumpModifier * JumpMaxHeight));
        vel += jumpVel;
        rb.velocity = vel;
    }
}