using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Anim_Script Player;
    public Collision feet;
    [SerializeField] public float bulletSpeed = 10f;



    public bool facingLeft;
    public bool onGround;

    void Start()
    {

    }

    void Update()
    {

        facingLeft = Player.facingLeft;
        onGround = feet.onGround;
        if (Input.GetKeyDown(KeyCode.Mouse0) && onGround)
        {

            float face = 1f;
            float pos = 0.5f;
            
            if(facingLeft)
            {
                face = -1f;
                pos = -1f;
            }
            else
            {
                face = 1f;
                pos = 1f;

            }

            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position +  pos * bulletSpawnPoint.right, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = face*bulletSpawnPoint.right * bulletSpeed;
           
        }

        Debug.Log("FacingLeft: " + facingLeft);

    }
}
