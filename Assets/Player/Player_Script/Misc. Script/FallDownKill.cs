using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownKill : MonoBehaviour
{
    public NewBehaviourScript PlayerScript;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            PlayerScript.OnFall();
        }
    }
}
