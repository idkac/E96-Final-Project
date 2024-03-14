using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Image[] hearts; // Array to hold references to 5 heart UI images
    public Image energyBar;
    public float maxHP = 5; // Maximum HP (number of hearts)
    public float currentHP = 5; // Current HP
    public int numberOfHearts = 5;

    public float maxEnergy = 100f; // Maximum energy level
    public float currentEnergy = 100f; // Current energy level

    public Image Weapon;
    public Sprite knife; // Default sprite
    public Sprite sword; // Alternate sprite
    public Sprite handgun;
    public Sprite shotgun;
    public string currentWeapon;

    public NewBehaviourScript player;

    void Start()
    {
        numberOfHearts = hearts.Length;
        maxHP = (float)numberOfHearts;
        currentHP = (float)numberOfHearts;
        currentWeapon = "Knife";
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = player.getHP()/100*numberOfHearts; // import the hp from player control and update every frame, convert hp from 100 scale to number of hearts
        // currentEnergy = PlayerEneger // import energy from player control
        // currentWeapon = PlayerWeapon // import weapon from player control

        // also get player skills


        // update hp
        for (int i = 1; i <= numberOfHearts; i++)
        {
            if (currentHP >= i)
            {
                // Heart is filled
                hearts[i-1].fillAmount = 1f;
                hearts[i-1].gameObject.SetActive(true);
            }
            else if (currentHP <i && currentHP > i-1)
            {
                // Heart is partially filled
                hearts[i-1].fillAmount = (float)(currentHP - (i-1));
                hearts[i-1].gameObject.SetActive(true);
            }
            else
            {
                // Heart is empty
                hearts[i-1].gameObject.SetActive(false);
            }
        }

        //update energy
        energyBar.fillAmount = currentEnergy / maxEnergy;

        // update weapon
        if(currentWeapon == "Sword")
        {
            Weapon.sprite = sword;
        }
        else if(currentWeapon == "Knife")
        {
            Weapon.sprite = knife;
        }
        else if (currentWeapon == "Handgun" || currentWeapon == "Gun")
        {
            Weapon.sprite = handgun;
        }
        else if (currentWeapon == "Shotgun")
        {
            Weapon.sprite = shotgun;
        }
        else
        {
            //default weapon
            Weapon.sprite = knife;
        }
    }

    public void setWeapon(string weapon)
    {
        currentWeapon = weapon;
    }
}
