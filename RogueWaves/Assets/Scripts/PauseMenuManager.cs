using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
   
   
   // Indicates whether the game in paused mode
   bool pauseGame;
   
   // Use this for initialization
   void Start () {  
      Hide(); 
      Debug.Log("Hide pause menu");
   }
   
   // Show the pause menu in pause mode (the
   // first option will say "Resume")
   public void ShowPause() {
      // Pause the game
      pauseGame = true;

      // Show the panel
      gameObject.SetActive(true);
   }
   
   
   // Hide the menu panel
   public void Hide() {
      // Deactivate the panel
      gameObject.SetActive(false);
      // Resume the game (if paused)
      pauseGame = false;
      Time.timeScale = 1f;
   }

      public void Resume() {
        Hide();
   }

      public void Quit() {
        SceneManager.LoadScene("LevelSelect");
   }
   
   // Update is called once per frame
   void Update () {  
      // If game is in pause mode, stop the timeScale value to 0
      if(pauseGame) {
         Time.timeScale = 0;
      }   
   }
}