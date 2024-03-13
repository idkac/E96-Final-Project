using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip damage;
    public AudioClip slash;
    public AudioClip gun1;
    public AudioClip gun2;
    public AudioClip jump;
    public AudioClip dash;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

}
