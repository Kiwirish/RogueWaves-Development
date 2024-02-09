using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crewmate2 : MonoBehaviour
{   

    public string name;
    public string description; 
    public int cooldown;

    public void ApplyPowerup(GameObject target)
    {
        int initial_DMG = target.GetComponent<Unit>().damage;
        int new_DMG = initial_DMG * 2;

        target.GetComponent<Unit>().damage = new_DMG;
        Debug.Log("Changed damage to: " + target.GetComponent<Unit>().damage);

    }
}