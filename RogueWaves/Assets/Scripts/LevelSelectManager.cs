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

    public void goToLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void goToLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void goToLevel5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}