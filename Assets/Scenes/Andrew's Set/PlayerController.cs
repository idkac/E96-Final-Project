using System.Collections;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
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


    public bool onWallRight, onWallLeft, onGround, hasDashed;
    private int allowedJumpCount;
    private struct Frameinputs
    {
        public float x, y;
        public int rawX, rawY;
    }

    private Frameinputs inputs;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collision>();
        allowedJumpCount = maxJumpCount;
    }

    // Update is called once per frame
    // movement for wasd and wall slide check
    void Update()
    {
        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");
        inputs.rawX = (int) Input.GetAxisRaw("Horizontal");
        inputs.rawY = (int) Input.GetAxisRaw("Vertical");
        
        rb.velocity = new Vector2(inputs.x * speed, rb.velocity.y);

        if(collision.onWall && !collision.onGround)
            wallSlide();

        if(collision.onGround)
        {
            allowedJumpCount = maxJumpCount;
            hasDashed = false;
        }
    }

//updated jump command to make it look nicer
    void OnJump()
    {
        allowedJumpCount--;
        Debug.Log(allowedJumpCount);
        if (allowedJumpCount > 0)
        {
        rb.velocity = Vector2.up * jumpVelocity;
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jump - 1) * Time.deltaTime;
        }
    }

    void wallSlide()
    {
        if (rb.velocity.y >= 0)
        {
            return;
        }
        if(collision.onWall && !collision.onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }
    }

//other way to get collision (to be implemented)
    void OnCollisionEnter2D(Collision2D tempcollision)
    {   
        foreach (ContactPoint2D contact in tempcollision.contacts)
        {
            if (Vector2.Dot(Vector2.left, contact.normal) >= 0.9)
            {
                onWallRight = true;
                onWallLeft = false;
            }
            else if (Vector2.Dot(Vector2.right, contact.normal) >= 0.9)
            {
                onWallLeft = true;
                onWallRight = false;
            }
            else
            {
                onWallLeft = false;
                onWallRight = false;
            }
        }
    }

    private Vector2 dashDirection;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;

    void OnDash() 
    {
        Debug.Log("this is running");
        if (!hasDashed)
        {
            dashDirection = new Vector2(inputs.x * speed, 0);
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        hasDashed = true;
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            rb.AddForce(dashDirection * dashSpeed);
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
