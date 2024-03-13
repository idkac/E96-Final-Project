using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform pellet1;
    public Transform pellet2;
    public GameObject bulletPrefab;
    public GameObject bullet1;
    public GameObject bullet2;
    public float bulletSpeed = 10f;
    bool onGround, facingLeft;

    public float HandGun_Delay = 0.12f;
    public float ShotGun_Delay = 0.5f;
    public float nextFire = 0.0f;


    float face = -1f;
    float pos = -1f;
   

    Transform Queen;
    Transform King;



    void Start()
    {
        Queen = transform.parent.parent;
        King = transform.parent;
       
    }



    void Update()
    {

        onGround = Queen.GetComponent<Collision>().onGround;
        facingLeft = King.GetComponent<Anim_Script>().facingLeft;
        

        Debug.Log("Equipped: " + King.GetComponent<Anim_Script>().equipped);

        Handgun();
        ShotGun();
    }

    void Handgun()
    {
        if (Time.time < nextFire)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && onGround && Anim_Script.Inventory.Handgun == King.GetComponent<Anim_Script>().equipped)
        {

            nextFire = Time.time + HandGun_Delay;


            if (facingLeft)
            {
                face = -1f;
                pos = -1f;
            }
            else
            {
                face = 1f;
                pos = 1f;
            }


            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position + pos * bulletSpawnPoint.transform.right, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = face * bulletSpawnPoint.right * bulletSpeed;
        }
    }


    void ShotGun()
    {
        if (Time.time < nextFire)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && onGround && Anim_Script.Inventory.Shotgun == King.GetComponent<Anim_Script>().equipped)
        {

            nextFire = Time.time + ShotGun_Delay;

           

            if (facingLeft)
            {
                face = -1f;
                pos = -1f;
            }
            else
            {
                face = 1f;
                pos = 1f;


            }

            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position + pos * bulletSpawnPoint.transform.right, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = face * bulletSpawnPoint.right * bulletSpeed;
            var slug1 = Instantiate(bullet1, pellet1.position + pos* 1.2f * pellet1.transform.right, pellet1.rotation);
            slug1.GetComponent<Rigidbody2D>().velocity = face * pellet1.right * bulletSpeed;
            var slug2 = Instantiate(bullet2, pellet2.position + pos * 1.2f * pellet2.transform.right, pellet2.rotation);
            slug2.GetComponent<Rigidbody2D>().velocity = face * pellet2.right * bulletSpeed;
        }
    }

    

}



