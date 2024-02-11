using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate3 : MonoBehaviour
{

    public BattleSystem battlesystem;
    public CrewManager crewmanager;
    public Text crewInfo;

    public string crewName;
    public string description;
    public int cooldown;

    int turnWhenInitiallyUsed = -10;

    public bool used = false;

    // specific components for powerup


    void Update()
    {
        crewmanager.usedInTurn[2] = used;
        if (!crewmanager.checkUsedCase() && (!used && (turnWhenInitiallyUsed + cooldown) >= battlesystem.turnvalue) || battlesystem.turnvalue == turnWhenInitiallyUsed + 1)
        {

            crewmanager.UpdateButtonColor("Crewmate3", Color.red);
        }
        else if (crewmanager.checkUsedCase() && (turnWhenInitiallyUsed + cooldown) <= battlesystem.turnvalue)
        {
            crewmanager.UpdateButtonColor("Crewmate3", Color.white);
        }
    }

    public void ApplyPowerup(GameObject target){

        if (crewmanager.checkUsedCase()){

            if (battlesystem.state == BattleState.PLAYERSHOOT && (turnWhenInitiallyUsed == 0 || turnWhenInitiallyUsed + cooldown <= battlesystem.turnvalue)){

                turnWhenInitiallyUsed = battlesystem.turnvalue;
                EnemyShooting enemyShooting = target.GetComponent<EnemyShooting>();

                if (enemyShooting != null){
                    enemyShooting.xOffset = 30f;
                    //enemyShooting.y1Offset = 20f;
                    //enemyShooting.y2Offset = 40f;
                    Debug.Log("Enemy is confused");
                }

                used = true;
                crewmanager.UpdateButtonColor("Crewmate3", Color.green);


            }
            else
            {
                Debug.Log("ON CD, Current turn: " + battlesystem.turnvalue + " ,Turn needed: " + (turnWhenInitiallyUsed + cooldown));
            }

        }else{
            Debug.Log("Already used one powerup this turn");
        }

    }

    void OnMouseOver()
    {
        crewInfo.text = $"Name: {name}\nDescription: {description}\nCooldown: {cooldown} seconds";
    }

    void OnMouseExit()
    {
        crewInfo.text = "";
    }

}