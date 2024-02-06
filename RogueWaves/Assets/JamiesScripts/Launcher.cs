using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Launcher : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
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
        
        // create projectile prefab at spawnpoint 
        Transform projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        
        // give it a velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.velocity = velocity;

        // Make the camera follow the projectile
        cinemachine.Follow = projectile.transform;

        // Wait for a certain duration (you can adjust this duration)
        yield return new WaitForSeconds(3f);

        // Reset the camera to the player after a certain duration
        if(projectile == null){
            cinemachine.Follow = player.transform;
        }

    }


}
