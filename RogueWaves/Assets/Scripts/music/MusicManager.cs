using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }

    public AudioSource audioSource;
    public AudioClip backgroundMusic; // Assign in the inspector
    public AudioClip bossFightMusic; // Assign in the inspector

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
            DontDestroyOnLoad(this.gameObject); // Keep music manager across scenes
        }

        // Ensure AudioSource is attached and set up
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Subscribe to music change events
        GameEvents.OnMusicChange += HandleMusicChange;

        // Optionally play default background music at game start
        PlayMusic(backgroundMusic);
    }

    private void OnDestroy()
    {
        // Clean up event subscription
        GameEvents.OnMusicChange -= HandleMusicChange;
    }

    private void HandleMusicChange(string musicType)
    {
        // Decide which music to play based on the event data
        switch (musicType)
        {
            case "bossFight":
                PlayMusic(bossFightMusic);
                break;
            // Add more cases as needed
            default:
                PlayMusic(backgroundMusic); // Fallback to background music
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