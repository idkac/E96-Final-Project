using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Anim_Script : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    private SpriteRenderer sp;
    private Transform parentBody;
    private Rigidbody2D rb;
    private Collision feet;
    RigidbodyConstraints2D originalConstraints;


    Vector2 inputVector;
    public bool facingLeft, hasDashed, locked, wallSlideR, wallSlideL, isFalling;
    float jumpCount;
    float maxJump;

    enum Inventory { Knife, Sword, Gun, Handgun, Shotgun };
    Inventory equipped = 0;
    int number = 1;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        parentBody = transform.parent;
        rb = parentBody.GetComponent<Rigidbody2D>();
        feet = parentBody.GetComponent<Collision>();
        originalConstraints = rb.constraints;
        maxJump = parentBody.GetComponent<NewBehaviourScript>().maxJumpCount;
    } 


// Update is called once per frame
void Update()
    {
        sp.flipX = facingLeft;

        //grabbing variables from other script <NewBehaviourScript> (PlayerController), that update overtime
        jumpCount = parentBody.GetComponent<NewBehaviourScript>().allowedJumpCount;
        hasDashed = parentBody.GetComponent<NewBehaviourScript>().hasDashed;
        wallSlideR = parentBody.GetComponent<NewBehaviourScript>().onWallRight;
        wallSlideL = parentBody.GetComponent<NewBehaviourScript>().onWallLeft;


        anim.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.5f && feet.onGround == true && locked == false);

        //weapon_selection
        if (Input.inputString != "")
        {

            bool is_a_number = Int32.TryParse(Input.inputString, out number);
           /* if (is_a_number && number >= 0 && number < 10)
            {
                
            }*/
        }
        idle();
        lockAim();
        slideAnim();
        falling();

        anim.SetBool("isFalling", isFalling);
        anim.SetBool("onGround", feet.onGround && !wallSlideL && !wallSlideR);


       
    }


    void OnMove(InputValue val)
    {

        inputVector = val.Get<Vector2>();
        if (inputVector.x == 1)
            facingLeft = false;
        if (inputVector.x == -1)
            facingLeft = true;
        if (locked == false)
        {
            equipped = Inventory.Knife;
            anim.SetInteger("Idle_State", 1);
        }




       
    }

    void idle()
    {

        //Debug.Log("number" + number);
        switch (number)
        {
            case 1:
                equipped = Inventory.Knife;
                anim.SetInteger("Idle_State", number);
                //Debug.Log(equipped);
                break;
            case 2:
                equipped = Inventory.Sword;
                anim.SetInteger("Idle_State", number);
                //Debug.Log(equipped);
                break;
            case 3:
                equipped = Inventory.Handgun;
                anim.SetInteger("Idle_State", number);
                //Debug.Log(equipped);
                break;
            case 4:
                equipped = Inventory.Shotgun;
                anim.SetInteger("Idle_State", number);
                //Debug.Log(equipped);
                break;
            default:
                break;

        }

    }

    void slideAnim()
    {
        anim.SetBool("isSlidingR", wallSlideR && !facingLeft && !feet.onGround);
        anim.SetBool("isSlidingL", wallSlideL && facingLeft && !feet.onGround);

    }
    
    void OnFire()
    {
        Debug.Log("ATTACKED");
        if(rb.velocity.magnitude < 1f && equipped != Inventory.Knife)
        {
            switch (equipped)
            {
                case Inventory.Sword:
                    if (facingLeft)
                    {
                        anim.Play("Sword_Attack_L", -1, 0f);
                        audioManager.PlaySFX(audioManager.slash);
                    }
                    else
                    {
                        anim.Play("Sword_Attack_R", -1, 0f);
                        audioManager.PlaySFX(audioManager.slash);
                    }
                    break;
                case Inventory.Handgun:
                    audioManager.PlaySFX(audioManager.gun1);
                    anim.Play("Handgun_Fire"); break;
                case Inventory.Shotgun:
                audioManager.PlaySFX(audioManager.gun2);
                    anim.Play("Shotgun_Fire"); break;
                default:
                    break;
            }

        }
        else
        {
            if(equipped == Inventory.Knife)
            {
                Debug.Log("SLASH");

                anim.Play("Slash");
            }
            
        }
    }

    void falling()
    {
        if (rb.velocity.y < -3f)
        {
            isFalling = true;
        }
        else
            isFalling = false;
    }
    bool lockAim()
    {
       
        if(Input.GetMouseButtonDown(1) && feet.onGround == true)
        {
            Debug.Log("Held");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            switch (equipped)
            {
                case Inventory.Sword:
                    anim.Play("Sword_Idle"); break;
                 
                case Inventory.Handgun:
                    anim.Play("Handgun_Aim");break;
                case Inventory.Shotgun:
                    anim.Play("Shotgun_Aim");break;
                default:
                    anim.Play("Idle");
                    break;

            }


            locked = true;
            return true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Released");
            rb.constraints = originalConstraints;
            locked = false;
            return false;
        }
        else
            return false;
   
    }
    void OnDash()
    {
        if (feet.onGround == false)
        {
            Debug.Log("DASH");
            if(!hasDashed)
            anim.Play("Dash");
        }
    }


    void OnJump()
    {
        if (jumpCount > 1)
            anim.Play("Jump");
    }

   
}
