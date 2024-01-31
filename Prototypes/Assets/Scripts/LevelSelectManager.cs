using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{

    public void GoToLevel()
    {
        // Load the "Level" scene
        SceneManager.LoadScene("Level");
    }

    
}