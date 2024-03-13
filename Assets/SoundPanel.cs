using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPanel : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
}
