using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitControl : MonoBehaviour
{
    public GameObject menu;
    public Transform StartPoint;
    public Transform Player;
    public NewBehaviourScript PlayerScript;

    public void RestartButton()
    {
        Debug.Log("Restart Pressed");
        if (Player != null && StartPoint!=null)
        {
            Debug.Log("Move to start");
            // Set the position of this object to match the position of the target object
            Player.position = StartPoint.position;
        }
        PlayerScript.setHP(100);
        menu.SetActive(false);
    }
}
