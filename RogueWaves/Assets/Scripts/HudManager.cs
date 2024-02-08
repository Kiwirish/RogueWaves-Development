using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{

    public GameObject pauseMenuPanel;
    public GameObject winMenuPanel;
    public GameObject loseMenuPanel;
    public BattleSystem battleSystem;

    MenuManager pauseMenu;
    MenuManager winMenu;
    MenuManager loseMenu;
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
}
