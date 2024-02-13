using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPMovement : MonoBehaviour
{
    // PVP player variables
    [SerializeField] GameObject[] players;
    [SerializeField] public int playerNumber;
    GameObject player;
    public bool yourTurn;
    Rigidbody2D rb;

    float speed = 3;

    public PVPBattleSystem battlesystem;
    public PlayerLauncher launcher;

    void Start(){
        if(playerNumber == 1){
            yourTurn = true;
            player = players[0];
            rb = players[0].GetComponent<Rigidbody2D>();
        }else if(playerNumber == 2){
            yourTurn = false;
            player = players[1];
            rb = players[1].GetComponent<Rigidbody2D>();
        }
    }
    // Update is called once per frame
    void Update()
    {   
        yourTurn = launcher.yourTurn;
        if(!yourTurn){
            HandleMovementInput();
        }
    }

    void HandleMovementInput(){
        if(battlesystem.state == PVPState.PLAYER1MOVE){
            // Player movement from input (it's a variable between -1 and 1) for
            // degree of left or right movement
            float movementInput = Input.GetAxis("Horizontal");
            // Move the player object
            transform.Translate(new Vector3(Time.deltaTime * speed * movementInput, 0, 0), Space.World);
        
        }else if(battlesystem.state == PVPState.PLAYER2MOVE){
            // Player movement from input (it's a variable between -1 and 1) for
            // degree of left or right movement
            float movementInput = Input.GetAxis("Horizontal");
            // Move the player object
            transform.Translate(new Vector3(Time.deltaTime * speed * movementInput, 0, 0), Space.World);
        }
        
    }
}
