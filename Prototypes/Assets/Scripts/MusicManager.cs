using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// start main background music

public class MusicManager : MonoBehaviour
{
    public AudioSource musicPlayer;
    public AudioClip backgroundMusic;

    void Start()
    {
        if (musicPlayer == null)
        {
            musicPlayer = GetComponent<AudioSource>();
            if (musicPlayer == null)
            {
                Debug.LogError("AudioSource component not found on the GameObject.");
                return;
            }
        }

        musicPlayer.clip = backgroundMusic;
        musicPlayer.loop = true; // Ensure the music loops
        musicPlayer.Play(); // Start playing the music


    }
}