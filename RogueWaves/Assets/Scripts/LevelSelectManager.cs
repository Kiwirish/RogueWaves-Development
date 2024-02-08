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

    public void goToLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

        public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}