using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{   
    public float damage;
    public float maxHP;
    public float currentHP;

    public bool triggered = false;

    public Slider playerHealth;

    public GameObject player;

    public GameObject cannonball;

    [SerializeField] private AudioSource hitSoundEffect;

    void OnTriggerEnter2D(Collider2D other){

        bool isPlayerProjectile = other.CompareTag("PlayerProjectile");
        bool isPlayer = player.CompareTag("Player");

        if(isPlayerProjectile && !(isPlayer)){
            hitSoundEffect.Play();
            triggered = true;
            Destroy(other.gameObject);
            Debug.Log("ENEMY WAS HIT BY A PLAYER CANNONBALL"); 

        }else if(!(isPlayerProjectile) && isPlayer){
            triggered = true;
            hitSoundEffect.Play();
            Destroy(other.gameObject); 
            Debug.Log("PLAYER WAS HIT BY A ENEMY CANNONBALL");  
        }
    } 

    public bool TakeDamage(float dmg){
        
        if(triggered){
            currentHP -= dmg;
            playerHealth.value = currentHP / maxHP;
            Debug.Log(currentHP + " " + maxHP + " " + dmg);
            Debug.Log(playerHealth.value);

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