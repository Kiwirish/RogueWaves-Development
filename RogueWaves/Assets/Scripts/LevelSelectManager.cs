using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{

    //public static LevelSelectManager Instance;

    //public GameObject tickLevel1; 
    //public GameObject tickLevel2; 
    //// add others

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject); 
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public static void ShowTickForLevel(string levelName)
    //{
    //    if (Instance == null) return;

    //    if (levelName == "Level1")
    //    {
    //        Instance.tickLevel1.SetActive(true);
    //    }
    //    else if (levelName == "Level2")
    //    {
    //        Instance.tickLevel2.SetActive(true);
    //    }
    //}

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