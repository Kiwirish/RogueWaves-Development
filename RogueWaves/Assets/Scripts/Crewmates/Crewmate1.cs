using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate1 : MonoBehaviour {   

    public BattleSystem battlesystem;

    public string name;
    public string description; 
    public int cooldown;

    int turnWhenInitiallyUsed = 0;

    public bool used = false;
    bool powerupUsedThisTurn;

    public int heal = 1; // specific components for powerup
    public Slider slider;


    // void FixedUpdate(){
    //     powerupUsedThisTurn = used || GameObject.Find("Crewmate2").GetComponent<Crewmate2>().used;
        
    //     Debug.Log("Powerups used this turn: " + powerupUsedThisTurn);
    // }

    public void ApplyPowerup(GameObject target){
        
        if(!(powerupUsedThisTurn) && (turnWhenInitiallyUsed == 0 || turnWhenInitiallyUsed + cooldown == battlesystem.turnvalue)){
            
            turnWhenInitiallyUsed = battlesystem.turnvalue;

            int current = target.GetComponent<Unit>().currentHP;
            int max = target.GetComponent<Unit>().maxHP;

            if (current + heal > max){
                current = max; // if heal will go above max health, heal to only max
            }else{
                current += heal; // otherwise heal based on value
            }
    
            target.GetComponent<Unit>().currentHP = current;
            slider.value = (float)current/max; // add to slider

            used = true;

            Debug.Log("Used poweup on turn " + turnWhenInitiallyUsed);

        }else{
            Debug.Log("ON CD, Current turn: " + battlesystem.turnvalue + " ,Turn needed: " + (turnWhenInitiallyUsed + cooldown));
        }
       
    }
}