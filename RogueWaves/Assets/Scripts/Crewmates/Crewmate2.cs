using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate2 : MonoBehaviour {   
    
    public BattleSystem battlesystem;
    public CrewManager crewmanager;
    public Text crewInfo;

    public string name;
    public string description; 
    public int cooldown;

    int turnWhenInitiallyUsed = 0;

    public bool used = false;

    // specific components for powerup


    void Update(){
        crewmanager.usedInTurn[1] = used;
    }

    public void ApplyPowerup(GameObject target){
        
        if(crewmanager.checkUsedCase()){
            

            if(battlesystem.state == BattleState.PLAYERSHOOT && (turnWhenInitiallyUsed == 0 || turnWhenInitiallyUsed + cooldown == battlesystem.turnvalue)){
            
            turnWhenInitiallyUsed = battlesystem.turnvalue;

            int initial_DMG = target.GetComponent<Unit>().damage;
            int new_DMG = initial_DMG * 2;

            target.GetComponent<Unit>().damage = new_DMG;
            Debug.Log("Changed damage to: " + target.GetComponent<Unit>().damage);
            
            used = true; 

        }else{
            Debug.Log("ON CD, Current turn: " + battlesystem.turnvalue + " ,Turn needed: " + (turnWhenInitiallyUsed + cooldown));
        }

        }else{
            Debug.Log("Already used one powerup this turn");
        }
       
    }

    void OnMouseOver(){
        crewInfo.text = $"Name: {name}\nDescription: {description}\nCooldown: {cooldown} seconds";
    }

    void OnMouseExit(){
        crewInfo.text = "";
    }

}