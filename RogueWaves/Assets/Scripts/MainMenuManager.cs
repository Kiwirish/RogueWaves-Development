using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] public GameObject main_menu;
    [SerializeField] public GameObject start_menu;
    [SerializeField] public GameObject help_menu;

    void Start(){
        ActivateMain();
    }

    public void StartGame(){
        ActivateStart();    
    }

    public void CampaignGame(){
        // Load the "Level" scene
        SceneManager.LoadScene("LevelSelect");
    }

    public void CustomPVC(){
        // Load the "PVC" scene
    }

    public void CustomPVP(){
        // Load the "PVP" scene
        SceneManager.LoadScene("PVP");
    }

    public void Back(){
        ActivateMain();
    }

    public void Help(){
        ActivateHelp();
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    void ActivateMain(){
        main_menu.SetActive(true);
        start_menu.SetActive(false);
        help_menu.SetActive(false);

    }

    void ActivateStart(){
        main_menu.SetActive(false);
        start_menu.SetActive(true);
        help_menu.SetActive(false);

    }

    void ActivateHelp(){
        main_menu.SetActive(false);
        start_menu.SetActive(false);
        help_menu.SetActive(true);
    }
}