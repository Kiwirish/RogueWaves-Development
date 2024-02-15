using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{

    public GameObject level1Tick, level2Tick, level3Tick, level4Tick, bossLevelTick;

    void Start()
    {
        CheckCompletedLevels();
    }

    void CheckCompletedLevels()
    {
        level1Tick.SetActive(GameManager.Instance.IsLevelCompleted("Level"));
        level2Tick.SetActive(GameManager.Instance.IsLevelCompleted("Level2"));
        level3Tick.SetActive(GameManager.Instance.IsLevelCompleted("Level3"));
        level4Tick.SetActive(GameManager.Instance.IsLevelCompleted("Level4"));
        bossLevelTick.SetActive(GameManager.Instance.IsLevelCompleted("Level5"));


    }
    // public static LevelSelectManager Instance { get; private set; }


    //public GameObject[] levelTicks; 

    //private void OnEnable()
    //{
    //    GameEvents.OnLevelBeaten += HandleLevelBeaten;
    //}

    //private void OnDisable()
    //{
    //    GameEvents.OnLevelBeaten -= HandleLevelBeaten;
    //}

    //private void HandleLevelBeaten(string levelName)
    //{
    //    levelTicks[0].SetActive(true);
    //}





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

    //[SerializeField] private GameObject[] levelTicks; 


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


    //public void MarkLevelComplete(int levelNumber)
    //{
    //    if (levelNumber >= 0 && levelNumber < levelTicks.Length)
    //    {
    //        levelTicks[levelNumber].SetActive(true); 
    //    }
    //}



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