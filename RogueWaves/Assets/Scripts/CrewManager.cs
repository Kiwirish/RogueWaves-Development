using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CrewManager : MonoBehaviour
{
    public static CrewManager Instance { get; private set; }

    public HashSet<string> crewmatesGained = new HashSet<string>();

    public Sprite[] sprites; // temp
    public Button[] buttons; // temp

    public Crewmate1 crew1;
    public Crewmate2 crew2;
    public Crewmate3 crew3;
    public Crewmate4 crew4;

    public Text crewmateInfoText;

    public bool[] usedInTurn = new bool[4];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        resetPowerupsForNextTurn();

        //GainCrewmate("Crewmate1"); // remove later (for testing)
        //GainCrewmate("Crewmate2"); // remove later (for testing)
        //GainCrewmate("Crewmate3"); // remove later (for testing)
        //GainCrewmate("Crewmate4"); // remove later (for testing)
    }

    public void GainCrewmate(string crewmateID)
    {
        if (!crewmatesGained.Contains(crewmateID))
        {
            crewmatesGained.Add(crewmateID);
            Debug.Log($"Crewmate gained: {crewmateID}");

            if (crewmateID == "Crewmate1")
            {
                buttons[0].GetComponent<Image>().sprite = sprites[0];
            }
            else if (crewmateID == "Crewmate2")
            {
                buttons[1].GetComponent<Image>().sprite = sprites[1];
            }
            else if (crewmateID == "Crewmate3")
            {
                buttons[2].GetComponent<Image>().sprite = sprites[2];
            }
            else if (crewmateID == "Crewmate4"){
                buttons[3].GetComponent<Image>().sprite = sprites[3];
            }
        }
     }

    public bool HasCrewmate(string crewmateID)
    {
        return crewmatesGained.Contains(crewmateID);
    }

    public void resetPowerupsForNextTurn(){
        crew1.used = false;
        crew2.used = false;
        crew3.used = false;
        crew4.used = false;
    }

    public bool checkUsedCase(){
        for(int i = 0; i < 4; i++){
            if(usedInTurn[i]){
                return false;
            }
        }
        return true;
    }

    public void UpdateButtonColor(string crewmateID, Color color)
    {
        if (crewmateID == "Crewmate1")
        {
            buttons[0].GetComponent<Image>().color = color;
        }
        else if (crewmateID == "Crewmate2")
        {
            buttons[1].GetComponent<Image>().color = color;
        }
        else if (crewmateID == "Crewmate3")
        {
            buttons[2].GetComponent<Image>().color = color;
        }
        else if (crewmateID == "Crewmate4")
        {
            buttons[3].GetComponent<Image>().color = color;
        }
    }

    
    public void UpdateCrewmateInfo(string name, string description, int cooldown, bool isActive)
    {
        // access Text in crewmateInfo and change it in this method,
        // called in 'ApplyPowerup' method for 5 seconds maybe
        if (isActive){

            crewmateInfoText.text = $"Name: {name}\nDescription: {description}\nCooldown: {cooldown} turns";
            crewmateInfoText.gameObject.SetActive(true);

        }else {

            crewmateInfoText.gameObject.SetActive(false);

        }

    }
}