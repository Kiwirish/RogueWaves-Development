using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameEvents
{
    public delegate void MusicChangeHandler(string music);

    public static event MusicChangeHandler OnMusicChange;

    public static void TriggerMusicChange(string music)
    {
        OnMusicChange?.Invoke(music);
    }
}
