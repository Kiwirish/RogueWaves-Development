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
    public GameObject target;

    Collider2D playerCollider;
    Collider2D targetCollider;


    void Start(){
        playerCollider = player.GetComponent<Collider2D>();
        targetCollider = target.GetComponent<Collider2D>();
    }


    void OnTriggerEnter2D(Collider2D other){
        //if(other.CompareTag("Cannonball") ){
            triggered = true;
            Destroy(other.gameObject);
            Debug.Log(player + " WAS HIT BY A CANNONBALL");
        //}
    } 

    public bool TakeDamage(int dmg){
        
        if(triggered){
            currentHP -= dmg;
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
