using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class EnemyShooting : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject start;
    public GameObject target;
    public GameObject cannonballPrefab; // Reference to the bullet prefab
    public CinemachineVirtualCamera cam;
    public GameObject player;
    [SerializeField] public GameObject enemy;
    public Text dialogueText;
    public Text statText;

    public float drag = 0.02f;

    private Vector3[] trajectoryPoints;

    private float v_vertex;
    private float y_vertex = 5f; // default value

    public float xOffset = 8f;
    bool randomise = true;

    public bool boss = false; 

    [SerializeField] private AudioSource shootSoundEffect; 

    public IEnumerator EnemyShoot(){
        statText.enabled = false;
        DrawArc();
        yield return StartCoroutine(Shoot());
        if(boss){
            enemy.GetComponent<Unit>().damage = 2;
        }else{
            enemy.GetComponent<Unit>().damage = 1;
        }
    }

    IEnumerator Shoot()
    {   

        cam.Follow = start.transform;
        yield return new WaitForSeconds(2f);
        dialogueText.text = "Enemy Fires";
        shootSoundEffect.Play();

        GameObject cannonball = Instantiate(cannonballPrefab, start.transform.position, Quaternion.identity);
        cannonball.tag = "EnemyProjectile";

        // Assign the Transform of the cannonball to the Follow property
        cam.Follow = cannonball.transform;

        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        float elapsedTime = 0f;
        int currentIndex = 0;

        while (currentIndex < trajectoryPoints.Length - 1) {
            if (rb != null) {
                rb.position = Vector3.Lerp(trajectoryPoints[currentIndex], trajectoryPoints[currentIndex + 1], elapsedTime / drag);
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= drag) {
                currentIndex++;
                elapsedTime = 0f;
            }
            yield return null;
        }

        if (rb != null) {
            rb.gravityScale = 10;
        }

        while (cannonball != null && cannonball.gameObject != null){
            yield return null;  // This is important for the coroutine to yield to the next frame
        }

        yield return new WaitForSeconds(2f);

        if(player != null){
            cam.Follow = player.transform;
        }

        if(boss){
            xOffset = 5f;
        }else{
            xOffset = 8f; // just for that one crewmate (hard coded right now)
        }
    
    }

    private void DrawArc()
    {   

        // Create vectors of the two x-axis positions
        Vector3 startPos = start.transform.position; // start is the enemy ship
        Vector3 targetPos = target.transform.position; // target is the player ship

        // Adjust the range of random values based on your requirements
        if(randomise){
            float randomXOffset = Random.Range(-xOffset, xOffset);
            targetPos.x += randomXOffset;
        }
        int random = Random.Range(3, 10);
        y_vertex = (float)random;

        float x_vertex = (startPos.x + targetPos.x) / 2; // the value of x at the vertex

        //int arcSmoothness = Mathf.RoundToInt(Mathf.Max((Mathf.Abs(startPos.x - targetPos.x)), y_vertex) * drag);
        int arcSmoothness = Mathf.RoundToInt(Vector2.Distance(targetPos, new Vector2(x_vertex, y_vertex)) * 2 * drag);
        trajectoryPoints = new Vector3[arcSmoothness];

        for (int i = 0; i < arcSmoothness; i++){
            float t = i / (float)(arcSmoothness - 1);
            Vector3 point = CalculateArc(startPos, targetPos, x_vertex, y_vertex, t);
            trajectoryPoints[i] = point; // list a set of points for the projectile to follow through
            //lr.SetPosition(i, point); // sets the point of the arc
        }
        
    }

    // Calculates the arc of each point
    private Vector3 CalculateArc(Vector3 startPos, Vector3 targetPos, float x_vertex, float y_vertex, float t)
    {
        // vertex (x, y) = (h, k)
        float h = x_vertex;
        float k = y_vertex;

        // quadratic formula
        float a = (startPos.y - k) / Mathf.Pow(startPos.x - h, 2);
        
        //float b = -2 * a * h;
        //float c = a * h * h + k;

        // Find a point between two lines using a fraction (interpolation)
        float x = Mathf.Lerp(startPos.x, targetPos.x, t);

        // Calculate the y position using the parabola equation
        float y = a * (x - h) * (x - h) + k;

        // Return the calculated point on the arc
        return new Vector3(x, y, -2f);
    }
} 