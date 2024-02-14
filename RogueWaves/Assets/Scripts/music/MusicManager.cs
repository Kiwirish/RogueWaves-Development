using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }

    public AudioSource audioSource;
    public AudioClip backgroundMusic; 
    public AudioClip bossFightMusic; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }

        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameEvents.OnMusicChange += HandleMusicChange;

        PlayMusic(backgroundMusic);
    }

    private void OnDestroy()
    {
        GameEvents.OnMusicChange -= HandleMusicChange;
    }

    private void HandleMusicChange(string musicType)
    {
        switch (musicType)
        {
            case "bossFight":
                PlayMusic(bossFightMusic);
                break;
            default:
                PlayMusic(backgroundMusic);
                break;
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (audioSource.clip != musicClip)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }
}