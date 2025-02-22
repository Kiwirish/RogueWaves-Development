using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate1 : MonoBehaviour {   

    public BattleSystem battlesystem;
    public CrewManager crewmanager;
    public Text crewInfo;

    public string crewName;
    public string description; 
    public int cooldown;

    int turnWhenInitiallyUsed = -10;

    public bool used = false;

    //private bool powerupJustActivated;

    public float heal = 1; // specific components for powerup
    public Slider slider;

    void Update()
    {
        crewmanager.usedInTurn[0] = used;

        if (!crewmanager.checkUsedCase() && (!used && (turnWhenInitiallyUsed + cooldown) >= battlesystem.turnvalue) || battlesystem.turnvalue == turnWhenInitiallyUsed+1)
        {
            
            crewmanager.UpdateButtonColor("Crewmate1", Color.red);
            crewmanager.UpdateCrewmateInfo("Healy Henry", "Instant heal", 2, false);

        }
        else if (crewmanager.checkUsedCase() && (turnWhenInitiallyUsed + cooldown) <= battlesystem.turnvalue)
        {
            crewmanager.UpdateButtonColor("Crewmate1", Color.white);
        }
        //and change turnWhenInitiallyUsed to like - 10


        // if (used)
        // {
        //    int turnsLeftOnCooldown = (turnWhenInitiallyUsed + cooldown) - battlesystem.turnvalue;

        //   if(turnsLeftOnCooldown > 0)
        //    {
        //       crewmanager.UpdateButtonColor("Crewmate1", Color.red);
        //   }
        //   else
        //   {
        //      crewmanager.UpdateButtonColor("Crewmate1", Color.white);
        //      used = false;

        //   }



        //if (!crewmanager.checkUsedCase() && !used && turnWhenInitiallyUsed+cooldown>=0)
        //{
        //    crewmanager.UpdateButtonColor("Crewmate1", Color.red);
        //}
        //if (crewmanager.checkUsedCase() && turnWhenInitiallyUsed + cooldown >= battlesystem.turnvalue) 
        //{
        //    crewmanager.UpdateButtonColor("Crewmate1", Color.white);
        //}

    }

        public void ApplyPowerup(GameObject target){
        
        if(crewmanager.checkUsedCase()){
            
            
            if(battlesystem.state == BattleState.PLAYERSHOOT && (turnWhenInitiallyUsed == -10 || turnWhenInitiallyUsed + cooldown <= battlesystem.turnvalue)){
            
            turnWhenInitiallyUsed = battlesystem.turnvalue;

            //powerupJustActivated = true;
            //used = true;

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

            crewmanager.UpdateButtonColor("Crewmate1", Color.green);

            crewmanager.UpdateCrewmateInfo("Healy Henry", "Instant heal", 2, true);



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