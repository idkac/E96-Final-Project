using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;


public class Anim_Script : MonoBehaviour
{
    // Start is called before the first frame update

    

    private Animator anim;
    private SpriteRenderer sp;
    private Transform ParentBody;
    private Rigidbody2D rb;


    private Vector2 inputVector;
    private bool facing_left;
    private bool on_Ground = true;
    private float weapon_use;


    enum Inventory { Knife, Sword, Gun, Handgun, Shotgun };
    Inventory equipped = 0;
    int number = 1;


    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ParentBody = transform.parent;
        rb = ParentBody.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sp.flipX = facing_left;
        anim.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.5f && on_Ground == true);

        //Debug.Log(rb.velocity);


        //weapon_selection

        if (Input.inputString != "")
        {

            bool is_a_number = Int32.TryParse(Input.inputString, out number);
           /* if (is_a_number && number >= 0 && number < 10)
            {
                
            }*/
        }
        idle();
    }


    void OnMove(InputValue val)
    {

        inputVector = val.Get<Vector2>();
        if (inputVector.x == 1)
            facing_left = false;
        if (inputVector.x == -1)
            facing_left = true;

        


        //Debug.Log(inputVector);
       // Debug.Log(facing_left);
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
                equipped = Inventory.Knife;
                anim.SetInteger("Idle_State", 1);
                //Debug.Log(equipped);
                break;

        }

    }
    
    void OnFire()
    {
        Debug.Log("ATTACKED");
        if(rb.velocity.magnitude < 1f && equipped != Inventory.Knife)
        {
            switch (equipped)
            {
                case Inventory.Sword:
                    anim.Play("Sword_Attack"); break;
                case Inventory.Handgun:
                    anim.Play("Handgun_Fire"); break;
                case Inventory.Shotgun:
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






}
