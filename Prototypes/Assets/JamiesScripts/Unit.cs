using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{   
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool triggered = false;

    public GameObject player;

    void OnTriggerEnter2D(Collider2D other){
        triggered = true;
        Debug.Log("HIT TAKEN");
    } 

    public bool TakeDamage(int dmg){
        
        if(triggered){
            currentHP -= dmg;
            if(currentHP <= 0){
                Destroy(player);
                return true;
            }
            Debug.Log("HP REMAINING: " + currentHP);
            triggered = false;
        }
        return false;
        
    }
}
