using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Dictionary<string, bool> levelCompletionStatus = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void MarkLevelCompleted(string levelName)
    {
        if (!levelCompletionStatus.ContainsKey(levelName))
        {
            levelCompletionStatus.Add(levelName, true);
        }
        else
        {
            levelCompletionStatus[levelName] = true;
        }
    }

    public bool IsLevelCompleted(string levelName)
    {
        if (levelCompletionStatus.ContainsKey(levelName))
        {
            return levelCompletionStatus[levelName];
        }
        return false;
    }
}
