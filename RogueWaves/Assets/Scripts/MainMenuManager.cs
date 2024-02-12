using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject main_menu;
    [SerializeField] private GameObject start_menu;
    [SerializeField] private GameObject help_menu;
    [SerializeField] private GameObject settings_menu;

    [SerializeField] private Dropdown map;
    [SerializeField] private Dropdown difficulty;
    [SerializeField] private Toggle powerups;

    private void Start()
    {
        ActivateMain();
    }

    public void StartGame()
    {
        ActivateStart();
    }

    public void CampaignGame()
    {
        // Load the "Level" scene
        SceneManager.LoadScene("LevelSelect");
    }

    public void ProtoCampaign(){
        SceneManager.LoadScene("WorldMap");  
    }

    public void CustomPVC()
    {
        // Load the "PVC" scene
        SceneManager.LoadScene("PVC");
    }

    public void CustomPVP()
    {
        // Load the "PVP" scene
        SceneManager.LoadScene("PVP");
    }

    public void Back()
    {
        ActivateMain();
    }

    public void Help()
    {
        ActivateHelp();
    }

    public void Settings()
    {
        ActivateSettings();
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    private void ActivateMain()
    {
        main_menu.SetActive(true);
        start_menu.SetActive(false);
        help_menu.SetActive(false);
        settings_menu.SetActive(false);
    }

    private void ActivateStart()
    {
        main_menu.SetActive(false);
        start_menu.SetActive(true);
        help_menu.SetActive(false);
        settings_menu.SetActive(false);
    }

    private void ActivateHelp()
    {
        main_menu.SetActive(false);
        start_menu.SetActive(false);
        help_menu.SetActive(true);
        settings_menu.SetActive(false);
    }

    private void ActivateSettings()
    {
        main_menu.SetActive(false);
        start_menu.SetActive(false);
        help_menu.SetActive(false);
        settings_menu.SetActive(true);
    }

    public void ConfirmSettings()
    {
        PlayerPrefs.SetInt("Map", map.value);
        PlayerPrefs.SetInt("Difficulty", difficulty.value);
        PlayerPrefs.SetInt("Powerups", powerups.isOn ? 1 : 0);

        Debug.Log("MAIN - Map: " + map.value + ", Difficulty: " + difficulty.value + ", Powerups: " + powerups.isOn);

        // Load the "PVC" scene
        SceneManager.LoadScene("PVC");
    }
}