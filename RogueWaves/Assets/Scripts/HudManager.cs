using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HudManager : MonoBehaviour
{

    public GameObject pauseMenuPanel;
    public GameObject winMenuPanel;
    public GameObject loseMenuPanel;
    public GameObject crewmateSelectionPanel;
    public GameObject crewmateButtonPrefab; 
    public Transform crewmateListParent; 
    public BattleSystem battleSystem;

    MenuManager pauseMenu;
    MenuManager winMenu;
    MenuManager loseMenu;
    MenuManager crewmateSelectionMenu;

    // Start is called before the first frame update
    void Start()
    {

        // Initialise the reference to the script object, which is a
        // component of the pause menu panel game object
        pauseMenu = pauseMenuPanel.GetComponent<MenuManager>();
        pauseMenu.Hide();
        
        winMenu = winMenuPanel.GetComponent<MenuManager>();
        winMenu.Hide();

        loseMenu = loseMenuPanel.GetComponent<MenuManager>();
        loseMenu.Hide();

        crewmateSelectionMenu = crewmateSelectionMenu.GetComponent<MenuManager>();
        crewmateSelectionMenu.Hide();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // If user presses ESC, show the pause menu in pause mode
            pauseMenu.Show();
        }

        if (battleSystem.state == BattleState.WIN)
        {
            winMenu.Show();
        }

        if (battleSystem.state == BattleState.LOSE)
        {
            loseMenu.Show();
        }

    }

    public void ShowCrewmateSelectionHUD()
    {
        // ClearCrewMateSelectionOptions();

        foreach (var crewmateID in CrewManager.Instance.crewmatesGained)
        {
            CreateCrewmateSelectionButton(crewmateID);
        }

        crewmateSelectionMenu.Show();


    }

    public void CreateCrewmateSelectionButton(string crewmateID)
    {
        GameObject buttonInstance = Instantiate(crewmateButtonPrefab, crewmateListParent);

        Text buttonText = buttonInstance.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = crewmateID; 
        }

        UnityEngine.UI.Button button = buttonInstance.GetComponent<UnityEngine.UI.Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnCrewmateSelected(crewmateID));
        }
    }

    public void OnCrewmateSelected(string crewmateID)
    {
        Debug.Log("Crewmate selected: " + crewmateID);
        
        //CrewManager.Instance.ActivateCrewmatePowerUp(crewmateID);

        crewmateSelectionMenu.Hide();
    }

    public void ClearCrewmateSelectionOptions()
    {
        foreach (Transform child in crewmateListParent)
        {
            Destroy(child.gameObject);
        }
    }


}
