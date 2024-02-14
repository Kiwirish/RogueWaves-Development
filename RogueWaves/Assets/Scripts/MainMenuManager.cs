using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject main_menu;
    [SerializeField] private GameObject start_menu;

    [SerializeField] private GameObject help1_menu;
    [SerializeField] private GameObject help2_menu;
    [SerializeField] private GameObject help3_menu;

    [SerializeField] private GameObject pvp_menu;
    [SerializeField] private GameObject pvc_menu;

    [SerializeField] private Dropdown map;
    [SerializeField] private Dropdown difficulty;
    [SerializeField] private Toggle powerups;

    [SerializeField] private Dropdown pvp_map;

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

    public void Back()
    {
        ActivateMain();
        help1_menu.SetActive(false);
        help2_menu.SetActive(false);
        help3_menu.SetActive(false);
        
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    public void PVC(){
        ActivatePVC();
    }

    public void PVP(){
        ActivatePVP();
    }

    private void ActivateMain()
    {
        main_menu.SetActive(true);
        start_menu.SetActive(false);
        pvp_menu.SetActive(false);
        pvc_menu.SetActive(false);
    }

    private void ActivateStart()
    {
        main_menu.SetActive(false);
        start_menu.SetActive(true);
        pvp_menu.SetActive(false);
        pvc_menu.SetActive(false);
    }

    private void ActivatePVP()
    {
        main_menu.SetActive(false);
        start_menu.SetActive(false);
        pvp_menu.SetActive(true);
        pvc_menu.SetActive(false);
    }

    private void ActivatePVC()
    {
        main_menu.SetActive(false);
        start_menu.SetActive(false);
        pvp_menu.SetActive(false);
        pvc_menu.SetActive(true);
    }

    public void Help1()
    {
        help1_menu.SetActive(true);
        help2_menu.SetActive(false);
        help3_menu.SetActive(false); 
    }

    public void Help2(){
        help1_menu.SetActive(false);
        help2_menu.SetActive(true);
        help3_menu.SetActive(false); 
    }
    public void Help3(){
        help1_menu.SetActive(false);
        help2_menu.SetActive(false);
        help3_menu.SetActive(true); 
    }

    public void ConfirmPVC()
    {
        PlayerPrefs.SetInt("Map", map.value);
        PlayerPrefs.SetInt("Difficulty", difficulty.value);
        PlayerPrefs.SetInt("Powerups", powerups.isOn ? 1 : 0);

        Debug.Log("MAIN - Map: " + map.value + ", Difficulty: " + difficulty.value + ", Powerups: " + powerups.isOn);

        // Load the "PVC" scene
        SceneManager.LoadScene("PVC");
    }

    public void ConfirmPVP()
    {
        PlayerPrefs.SetInt("Map", pvp_map.value);

        // Load the "PVP" scene
        SceneManager.LoadScene("PVP");
    }
}