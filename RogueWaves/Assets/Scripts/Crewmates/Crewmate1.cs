using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate1 : MonoBehaviour
{   

    public string name;
    public string description; 
    public int cooldown;

    public int heal = 1;
    public Slider slider;

    public void ApplyPowerup(GameObject target)
    {
        int current = target.GetComponent<Unit>().currentHP;
        int max = target.GetComponent<Unit>().maxHP;

        if (current + heal > max){
            current = max;
        }else{
            current += heal;
        }
    
        target.GetComponent<Unit>().currentHP = current;
        slider.value = (float)current/max;

        Debug.Log("New currentHP: " + target.GetComponent<Unit>().currentHP);

    }
}