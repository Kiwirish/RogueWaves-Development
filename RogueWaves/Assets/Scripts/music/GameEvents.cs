using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameEvents
{
    public delegate void MusicChangeHandler(string music);

    public static event MusicChangeHandler OnMusicChange;

    public delegate void LevelBeatenHandler(string levelName);

    public static event LevelBeatenHandler OnLevelBeaten;

    public static void TriggerMusicChange(string music)
    {
        OnMusicChange?.Invoke(music);
    }
    public static void LevelBeaten(string levelName)
    {
        OnLevelBeaten?.Invoke(levelName);
    }
}
