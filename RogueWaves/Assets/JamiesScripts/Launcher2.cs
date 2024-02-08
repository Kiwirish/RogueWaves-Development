using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Launcher2 : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float launchForce = 2f;
    [SerializeField] float trajectoryTimeStep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    [SerializeField] private AudioSource shootSoundEffect;

    Vector2 velocity, startMousePosition, currentMousePosition;

    // Added mouse aiming
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject cannon;
    public Camera cam;
    Vector2 mousePos;

    public float power;
    public float angle;

    // Added camera
    [SerializeField] CinemachineVirtualCamera cinemachine;

    public BattleSystem battleSystem;
    public CameraSwitch camswitch;
    public ButtonFrameCheck check;

    public Text dialogueText;
    public Text statText;
    
    bool zoom;

    LineRenderer lastLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startMousePosition = cannon.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        zoom = camswitch.ZoomView;
        //inFrame = check.inFrame;

        //Debug.Log("Zoomed In: " + zoom + " , Deactivated shooting: " );
        // Check if it's the player's turn before allowing the launcher to be used
        if (battleSystem.state == BattleState.PLAYERSHOOT)
        {   
            statText.enabled = true;
            HandleLauncherInput();
        }

    }

    void HandleLauncherInput()
{
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
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float timeElapsed = i * trajectoryTimeStep;
            Vector3 position = (Vector2)spawnPoint.position + (velocity * timeElapsed) + (0.5f * Physics2D.gravity * timeElapsed * timeElapsed);
            positions[i] = position;
        }
        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(positions);
    }

    void ClearTrajectory()
    {
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

        battleSystem.gameIdle();
        dialogueText.text = "Player Fires";

        // create projectile prefab at spawnpoint 
        Transform projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        projectile.tag = "PlayerProjectile";
        
        // give it a velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.velocity = velocity;

        // Make the camera follow the projectile
        cinemachine.Follow = projectile.transform;

        while (projectile != null && projectile.gameObject != null){
            yield return null;  // This is important for the coroutine to yield to the next frame
        }

        
        StartCoroutine(battleSystem.endPlayerShoot());
        yield return new WaitForSeconds(1f);
        cinemachine.Follow = player.transform;
    }

}
