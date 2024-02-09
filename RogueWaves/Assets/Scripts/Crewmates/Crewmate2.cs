using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate2 : MonoBehaviour {   
    
    public BattleSystem battlesystem;

    public string name;
    public string description; 
    public int cooldown;

    int turnWhenInitiallyUsed = 0;

    public bool used = false;
    bool powerupUsedThisTurn;

    // specific components for powerup


    // void Update(){
    //     powerupUsedThisTurn = used || GameObject.Find("Crewmate1").GetComponent<Crewmate1>().used;
        
    // }

    public void ApplyPowerup(GameObject target){
        
        if((turnWhenInitiallyUsed == 0 || turnWhenInitiallyUsed + cooldown == battlesystem.turnvalue)){
            
            turnWhenInitiallyUsed = battlesystem.turnvalue;

            int initial_DMG = target.GetComponent<Unit>().damage;
            int new_DMG = initial_DMG * 2;

            target.GetComponent<Unit>().damage = new_DMG;
            Debug.Log("Changed damage to: " + target.GetComponent<Unit>().damage);
            
            used = true; 

        }else{
            Debug.Log("ON CD, Current turn: " + battlesystem.turnvalue + " ,Turn needed: " + (turnWhenInitiallyUsed + cooldown));
        }
       
    }

}