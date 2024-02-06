using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{   
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool triggered = false;

    public GameObject player;
    public Slider playerHealth;

    void OnTriggerEnter2D(Collider2D other){
        triggered = true;
        Destroy(other.gameObject);
        Debug.Log(player + " WAS HIT BY A CANNONBALL");
    } 

    public bool TakeDamage(int dmg){
        
        if(triggered){
            currentHP -= dmg;
            playerHealth.value = (float)currentHP / maxHP;

            if(currentHP <= 0){
                Destroy(player);
                return true;
            }
            Debug.Log(player + " HP REMAINING: " + currentHP);
            triggered = false;
        }
        return false;
        
    }
}
