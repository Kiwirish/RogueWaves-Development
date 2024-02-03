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
    public Text dialogueText;

    public float speed;
    private Vector3[] trajectoryPoints;

    private float v_vertex;
    private float y_vertex = 5f; // default value

    public float xOffset = 5f;
    bool randomise = true;

    // Update is called once per frame
    void Update()
    {   
        // Check if the space button is pressed
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // Fire a bullet along the parabola
        //     DrawArc();
        //     StartCoroutine(Shoot());
        // }
        if(Input.GetKeyDown(KeyCode.R)){
            randomise = true;
            Debug.Log("Randomised");
        }
        if(Input.GetKeyDown(KeyCode.T)){
            randomise = false;
            Debug.Log("Not randomised");
        }
    }

    public IEnumerator  EnemyShoot(){
        DrawArc();
        yield return StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {   

        cam.Follow = start.transform;
        yield return new WaitForSeconds(2f);
        dialogueText.text = "Enemy Fires";

        GameObject cannonball = Instantiate(cannonballPrefab, start.transform.position, Quaternion.identity);

        // Assign the Transform of the cannonball to the Follow property
        cam.Follow = cannonball.transform;

        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        for (int i = 0; i < trajectoryPoints.Length; i++)
        {
            if(rb != null){
                rb.position = trajectoryPoints[i];
            }
            yield return null;
        }

        Destroy(cannonball);

        yield return new WaitForSeconds(2f);


        // Reset the camera to the main camera when the cannonball is destroyed
        if(player != null){
            cam.Follow = player.transform;
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

            int random = Random.Range(3, 10);
            y_vertex = (float)random;
        }

        float x_vertex = (startPos.x + targetPos.x) / 2; // the value of x at the vertex

        Debug.Log(Mathf.RoundToInt(Mathf.Abs(startPos.x - targetPos.x)) * (Mathf.RoundToInt(y_vertex) * 30));
        int arcSmoothness = Mathf.RoundToInt((Mathf.Abs(startPos.x - targetPos.x)) * (y_vertex * speed));

        trajectoryPoints = new Vector3[arcSmoothness];

        //lr.positionCount = arcSmoothness; // number of points displaying the arc
        for (int i = 0; i < arcSmoothness; i++)
        {
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