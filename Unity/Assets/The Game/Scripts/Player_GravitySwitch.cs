using UnityEngine;

public class Player_GravitySwitch : MonoBehaviour
{
    public static event System.Action BeforeFlip;
    public static event System.Action AfterFlip;

    Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var input = new Vector2(Input.GetAxisRaw("FlipX"), Input.GetAxisRaw("FlipY"));
        if (input == Vector2.zero)
            return;


        SetDirection(input);
    }

    private void SetDirection(Vector2 direction)
    {
        if (direction.x != 0)
            direction.y = 0;

        Physics2D.gravity = direction.normalized * Physics2D.gravity.magnitude;
        rb.velocity = Quaternion.FromToRotation(rb.velocity, Physics2D.gravity) * rb.velocity;

    }
}
