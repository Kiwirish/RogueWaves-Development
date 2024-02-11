using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate1 : MonoBehaviour {   

    public BattleSystem battlesystem;
    public CrewManager crewmanager;
    public Text crewInfo;

    public string name;
    public string description; 
    public int cooldown;

    int turnWhenInitiallyUsed = 0;

    public bool used = false;

    public float heal = 1; // specific components for powerup
    public Slider slider;

    void Update(){
        crewmanager.usedInTurn[0] = used;
    }

    public void ApplyPowerup(GameObject target){
        
        if(crewmanager.checkUsedCase()){
            
            
            if(battlesystem.state == BattleState.PLAYERSHOOT && (turnWhenInitiallyUsed == 0 || turnWhenInitiallyUsed + cooldown <= battlesystem.turnvalue)){
            
            turnWhenInitiallyUsed = battlesystem.turnvalue;

            float current = target.GetComponent<Unit>().currentHP;
            float max = target.GetComponent<Unit>().maxHP;

            if (current + heal > max){
                current = max; // if heal will go above max health, heal to only max
            }else{
                current += heal; // otherwise heal based on value
            }
    
            target.GetComponent<Unit>().currentHP = current;
            slider.value = (float)current/max; // add to slider

            used = true;

            Debug.Log("Healed 1HP");

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