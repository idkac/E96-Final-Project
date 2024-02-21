using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collision collision;
    [SerializeField] float speed = 2f;
    [SerializeField] float fall = 2.5f;
    [SerializeField] float jump = 2f;
    [SerializeField] float jumpVelocity = 3f;
    [SerializeField] float slideSpeed = 0.2f;
    [SerializeField] int maxJumpCount = 2;
    private int allowedJumpCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collision>();
        allowedJumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x,y);

        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if(collision.onWall && !collision.onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }

        if(collision.onGround)
            allowedJumpCount = maxJumpCount;
    }

    void OnJump()
    {
        allowedJumpCount--;
        if (allowedJumpCount > 0)
        {
        rb.velocity = Vector2.up * jumpVelocity;
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jump - 1) * Time.deltaTime;
        }
    }
}
