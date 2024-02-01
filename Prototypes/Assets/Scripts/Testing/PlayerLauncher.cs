using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float launchForce = 2f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    // Mouse controls
    Vector2 velocity, startMousePosition, currentMousePosition, mousePos;

    // Cameras
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] Camera cam;

    // PVP player variables
    [SerializeField] GameObject[] players;
    [SerializeField] public int playerNumber;
    GameObject player;
    bool yourTurn;
    Rigidbody2D rb;

    // Scripts
    [SerializeField] public PVPBattleSystem system;
    [SerializeField] public PVPMovement move;

    // Start is called before the first frame update
    void Start()
    {
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

        if(yourTurn){

            if (Input.GetMouseButtonDown(0)){
                startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(0)){
                currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                velocity = (startMousePosition - currentMousePosition) * launchForce;
                DrawTrajectory();
            }
            if (Input.GetMouseButtonUp(0)){
                StartCoroutine(ShootProjectile());
                ClearTrajectory();
            }

            // Added mouse aiming 
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position; 
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
            spawnPoint.rotation = Quaternion.Euler(0f, 0f, angle);  
        }
        
    }

    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for(int i = 0; i < trajectoryStepCount; i++)
        {
            float timeElapsed = i * trajectoryTimeStep;
            Vector3 position = (Vector2)spawnPoint.position + (velocity * timeElapsed) + (0.5f * Physics2D.gravity * timeElapsed * timeElapsed);
            positions[i] = position;
        }
        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(positions);
    }

    void ClearTrajectory() {

        lineRenderer.positionCount = 0;

    }

    IEnumerator ShootProjectile()
    {   
        
        if(playerNumber == 1){
            system.player1Attacks();
        }else if(playerNumber == 2){
            system.player2Attacks();
        }

        // create projectile prefab at spawnpoint 
        Transform projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        
        // give it a velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.velocity = velocity;

        // Make the camera follow the projectile
        cinemachine.Follow = projectile.transform;

        while (projectile != null && projectile.gameObject != null){
            //Debug.Log("Looping"); // loop
            yield return null;  // This is important for the coroutine to yield to the next frame
        }
        yield return new WaitForSeconds(2f);

        //Debug.Log("Object destroyed");

        if (playerNumber == 1 && yourTurn){
            // Change turn to Player 2
            yourTurn = false;
            players[1].GetComponent<PlayerLauncher>().yourTurn = true;
            cinemachine.Follow = players[1].GetComponent<PlayerLauncher>().players[1].transform;
            StartCoroutine(system.endPlayer1Turn());
        }else if (playerNumber == 2 && yourTurn){
             // Change turn to Player 1
            yourTurn = false;
            players[0].GetComponent<PlayerLauncher>().yourTurn = true;
            cinemachine.Follow = players[0].GetComponent<PlayerLauncher>().players[0].transform;
            StartCoroutine(system.endPlayer2Turn());
        }
        //Debug.Log("Player turn passed");
    }
}



