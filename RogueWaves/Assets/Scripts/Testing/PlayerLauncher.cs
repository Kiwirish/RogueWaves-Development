using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float launchForce = 2f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    [SerializeField] private AudioSource shootSoundEffect;

    // Mouse controls
    Vector2 velocity, startMousePosition, currentMousePosition, mousePos;

    // Cameras
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] Camera cam;

    // PVP player variables
    [SerializeField] GameObject[] players;
    [SerializeField] public int playerNumber;
    public GameObject cannon;
    GameObject player;
    public bool yourTurn;
    Rigidbody2D rb;

    public float power;
    public float angle;

    // Scripts
    [SerializeField] public PVPBattleSystem battleSystem;
    [SerializeField] public PVPMovement move;
    public CameraSwitch camswitch;

    [SerializeField] public Text statText;

    bool zoom; 

    bool active = true; // for testing

    LineRenderer lastLineRenderer;

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

        zoom = camswitch.ZoomView;

        if(yourTurn){

            if (battleSystem.state == PVPState.PLAYER1SHOOT && active && !EventSystem.current.IsPointerOverGameObject()){   
                statText.enabled = true;
                HandleLauncherInput();
            }else if(battleSystem.state == PVPState.PLAYER2SHOOT && active && !EventSystem.current.IsPointerOverGameObject()){   
                statText.enabled = true;
                HandleLauncherInput();
            }   
        }
    }

    void HandleLauncherInput(){
        // Ensure cinemachine is not null before accessing its components
    if (cinemachine != null)
    {
        // Added mouse aiming 
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        angle = (Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f + 90f) % 360f;

        cannon.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f);

        if (zoom)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = cannon.transform.position;
            }
            if (Input.GetMouseButton(0))
            {   
                currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                velocity = (startMousePosition - currentMousePosition) * launchForce;
                power = Mathf.Round(velocity.magnitude * 10f) / 10f; 
                DrawTrajectory();
                //Debug.Log(Mathf.RoundToInt(angle) + " " + power);

                statText.text = "[ " + Mathf.RoundToInt(angle) + "° , " + power + "m/s ]";
            }

            if (Input.GetMouseButtonUp(0))
            {
                shootSoundEffect.Play();
                StartCoroutine(ShootProjectile());
                //shootSoundEffect.Play();
                NewLineRenderer();
                ClearTrajectory();
            }
            
        }else{
            ClearTrajectory();
        }
    }
    else
        {
            // Handle the case where cinemachine is null (optional, based on your requirements)
            Debug.LogError("Cinemachine is not assigned.");
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

    void NewLineRenderer(){
        if(lineRenderer != null){
    
            if(lastLineRenderer != null){
                Destroy(lastLineRenderer.gameObject);
            }

            GameObject newLineObject = new GameObject("LastLineRenderer");
            lastLineRenderer = newLineObject.AddComponent<LineRenderer>();

            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            lastLineRenderer.positionCount = positions.Length;
            lastLineRenderer.SetPositions(positions);

            lastLineRenderer.startWidth = 0.2f;
            lastLineRenderer.endWidth = 0.1f;

            lastLineRenderer.startColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            lastLineRenderer.endColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            lastLineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        }
    }

    IEnumerator ShootProjectile()
    {

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
       yield return new WaitForSeconds(1f);

        //Debug.Log("Object destroyed");

        if (playerNumber == 1 && yourTurn){
            // Change turn to Player 2
            yourTurn = false;
            players[1].GetComponent<PlayerLauncher>().yourTurn = true;
            cinemachine.Follow = players[0].GetComponent<PlayerLauncher>().players[0].transform;
            StartCoroutine(battleSystem.endPlayer1Turn());
            yield return new WaitForSeconds(4f);
            cinemachine.Follow = players[1].GetComponent<PlayerLauncher>().players[1].transform;

        }else if (playerNumber == 2 && yourTurn){
            // Change turn to Player 1
            yourTurn = false;
            players[0].GetComponent<PlayerLauncher>().yourTurn = true;
            cinemachine.Follow = players[1].GetComponent<PlayerLauncher>().players[1].transform;
            StartCoroutine(battleSystem.endPlayer2Turn());
            yield return new WaitForSeconds(4f);
            cinemachine.Follow = players[0].GetComponent<PlayerLauncher>().players[0].transform;

        }
        //Debug.Log("Player turn passed");
        players[0].GetComponent<Unit>().damage = 1; // just for that one crewmate (hard coded right now)
        players[1].GetComponent<Unit>().damage = 1; // just for that one crewmate (hard coded right now)

    }
}
