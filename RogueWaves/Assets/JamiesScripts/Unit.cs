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

    public Slider playerHealth;

    public GameObject player;

    public GameObject cannonball;

    void OnTriggerEnter2D(Collider2D other){

        bool isPlayerProjectile = other.CompareTag("PlayerProjectile");
        bool isPlayer = player.CompareTag("Player");

        if(isPlayerProjectile && !(isPlayer)){
            triggered = true;
            Destroy(other.gameObject);
            Debug.Log("ENEMY WAS HIT BY A PLAYER CANNONBALL");   
        }else if(!(isPlayerProjectile) && isPlayer){
            triggered = true;
            Destroy(other.gameObject); 
            Debug.Log("PLAYER WAS HIT BY A ENEMY CANNONBALL");  
        }
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