using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField]Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float launchForce = 2f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    Vector2 velocity, startMousePosition, currentMousePosition;

    // Added mouse aiming
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject cannon;
    public Camera cam;
    Vector2 mousePos;

    // Added camera
    [SerializeField] CinemachineVirtualCamera cinemachine;

    // Added PVP
    public bool pvp;
    public int playerNumber;
    
    public PVPBattleSystem system;

    bool yourTurn;
    
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        if(playerNumber == 1){
            yourTurn = true;
        }else if(playerNumber == 2){
            yourTurn = false;
        }else{
            Debug.Log("Something went wrong");
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
            cannon.transform.rotation = Quaternion.Euler(0f, 0f, angle);  
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
        
        if(pvp && playerNumber == 1){
            system.player1Attacks();
        }else if(pvp && playerNumber == 2){
            system.player2Attacks();
        }else{

        }

        // create projectile prefab at spawnpoint 
        Transform projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        
        // give it a velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.velocity = velocity;

        // Make the camera follow the projectile
        cinemachine.Follow = projectile.transform;

        yield return new WaitForSeconds(3f);

        if (pvp){
            if (playerNumber == 1 && yourTurn){
                //Debug.Log("Changed turn to Player 2"); 
                yourTurn = false;
                player2.GetComponent<PlayerLauncher>().yourTurn = true;
                cinemachine.Follow = player2.GetComponent<PlayerLauncher>().player2.transform;
                StartCoroutine(system.endPlayer1Turn());
            }else if (playerNumber == 2 && yourTurn){
                //Debug.Log("Changed turn to Player 1"); 
                yourTurn = false;
                player1.GetComponent<PlayerLauncher>().yourTurn = true;
                cinemachine.Follow = player1.GetComponent<PlayerLauncher>().player1.transform;
                StartCoroutine(system.endPlayer2Turn());
            }
        }
        yield break;
    }
}


