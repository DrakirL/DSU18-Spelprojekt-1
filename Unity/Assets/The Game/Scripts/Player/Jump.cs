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

    public event System.Action OnJump;

    bool isEnabled = true;
    private void Awake()
    {
        var death = GetComponent<Player_Death>();
        death.BeforeDie += Disable;
        death.AfterDie += Reenable;

        var col = GetComponentInChildren<BoxCollider2D>();
        colliderHeight = col.bounds.extents.y;
        colliderOffset = col.offset;
        colWidth = col.bounds.extents.x * 2;

        rb = GetComponent<Rigidbody2D>();
    }

    void Disable(CauseOfDeath c)
    {
        isEnabled = false;
    }

    void Reenable()
    {
        isEnabled = true;
    }

    private float colWidth;

    [SerializeField]
    LayerMask GroundMask;

    [SerializeField]
    private int HorizontalRaycastCount;

    [SerializeField]
    [Range(0f, 0.1f)]
    private float skinWidth;
    public bool IsJumping { get; private set; }
    [SerializeField]
    public bool isGrounded = false;

    public event System.Action HitGround;

    // Update is called once per frame
    private void Update()
    {
        if (!isEnabled)
            return;

        var origin = transform.position + colliderOffset;
        origin.x += skinWidth - colWidth / 2f;

        for (int i = 0; i < HorizontalRaycastCount; i++)
        {
            if (rb.velocity.y < 0)
                continue;

            var hit = Physics2D.Raycast(
                origin: origin,
                direction: Physics2D.gravity.normalized,
                distance: colliderHeight + skinWidth,
                layerMask: GroundMask
            );

            Debug.DrawLine(origin, (Vector2)origin + Physics2D.gravity.normalized * (colliderHeight + skinWidth)) ;
            origin += Vector3.right * ((colWidth - (2*skinWidth)) / (HorizontalRaycastCount - 1));

            isGrounded = hit;

            if (isGrounded)
            {
                Land(hit.transform.gameObject);
                HitGround();
                break;
            }
        }
        
        

        if (ShouldJump())
        {
            IsJumping = true;

            OnJump?.Invoke();

            JumpToHeight();
        }


        /*
        if (Input.GetKey(KeyCode.Space) && isJumping && rb.velocity.y < 0)
        {
            var vel = rb.velocity;
            vel += -Physics2D.gravity * (1 - LowJumpModifier) * Time.deltaTime;
            rb.velocity = vel;
        }*/
    }

    void Land(GameObject surface)
    {
        //Determine the velocity

        if (surface.GetComponent<Breakable>())
        {
            var breakable = surface.GetComponent<Breakable>();

            if (HardLanding && breakable.CanBeBrokenByPlayer)
            {
                breakable.GetBroken();
                isGrounded = false;

            }
        }

        IsJumping = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitGroundVel = -collision.relativeVelocity.y;
    }

    private bool ShouldJump()
    {
        bool velocityIsLowEnough = false;

        if (Physics2D.gravity.x != 0)
            velocityIsLowEnough = rb.velocity.x <= velocitySensitivity;
        else
            velocityIsLowEnough = rb.velocity.y <= velocitySensitivity;

        return Input.GetKey(KeyCode.Space) && isGrounded && !IsJumping && velocityIsLowEnough;
    }

    private void JumpToHeight()
    {
        var vel = rb.velocity;

        var jumpVel = -Physics2D.gravity.normalized * Mathf.Sqrt(Mathf.Abs(2 * Physics2D.gravity.magnitude * LowJumpModifier * JumpMaxHeight));
        vel += jumpVel;
        rb.velocity = vel;
    }
}