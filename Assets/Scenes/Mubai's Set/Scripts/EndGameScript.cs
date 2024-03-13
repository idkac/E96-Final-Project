using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    public GameObject exitMenu;
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            exitMenu.SetActive(true);
            // Application.Quit();
        }
    }

    public void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
