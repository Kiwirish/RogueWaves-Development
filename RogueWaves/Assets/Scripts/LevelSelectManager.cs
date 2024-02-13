using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{

    public static LevelSelectManager Instance { get; private set; }

    //public GameObject tickLevel1; 
    //public GameObject tickLevel2; 
    //// add others
    //public Sprite[] sprites; // temp
    //public Button[] buttons; // temp


    //public GameObject tick1;
    //public GameObject tick2;
    //public GameObject tick3;
    //public GameObject tick4;
    //public GameObject tick5;

    [SerializeField] private GameObject[] levelTicks; // Assign these in the Inspector


    //public void GainTick(string level) {

    //    if(level == "Level") {
    //        buttons[0].GetComponent<Image>().sprite = sprites[0];
    //        //lastLineRenderer.startColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    //    }
    //    else
    //    {
    //        buttons[0].GetComponent<Image>().sprite = sprites[1];

    //    }


    //}


    public void MarkLevelComplete(int levelNumber)
    {
        if (levelNumber >= 0 && levelNumber < levelTicks.Length)
        {
            levelTicks[levelNumber].SetActive(true); 
        }
    }



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