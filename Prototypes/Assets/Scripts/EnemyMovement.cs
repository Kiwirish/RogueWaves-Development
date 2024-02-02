using System.Collections;
using UnityEngine;
using Cinemachine;

public class EnemyMovement : MonoBehaviour
{
    public float movementValue;

    private Rigidbody2D rb;

    //private bool moving = false;

    public CinemachineVirtualCamera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.UpArrow) && !moving)
        // {
        //     moving = true;
        //     StartCoroutine(EnemyMovementPhase());
        // }
    }

    public void EnemyMove(){
        StartCoroutine(EnemyMovementPhase()); 
    }

    IEnumerator EnemyMovementPhase()
    {   
        
        cam.Follow = rb.transform;

        float random = Random.Range(-movementValue, movementValue);

        while(-movementValue/4f < random && random < movementValue/4f){
            Debug.Log("Retry: " + random);
            random = Random.Range(-movementValue, movementValue);
        }

        float newPos = rb.position.x + random;
        Debug.Log("Random: " + newPos);

        while (Mathf.Abs(rb.position.x - newPos) > 0.01f)
        {   
            float pos = rb.position.x;
            if(random < 0){
                rb.position = new Vector3(pos -= 0.01f, rb.position.y, 0);
            }else{
                rb.position = new Vector3(pos += 0.01f, rb.position.y, 0);
            }
            yield return null;
        }
        rb.position = new Vector3(newPos, rb.position.y, 0);
        yield return new WaitForSeconds(1f);

        //moving = false;
    }
}