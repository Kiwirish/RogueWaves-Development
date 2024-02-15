using System.Collections;
using UnityEngine;
using Cinemachine;

public class EnemyMovement : MonoBehaviour
{
    public float movementValue;

    private Rigidbody2D rb;

    public GameObject player;

    public CinemachineVirtualCamera cam;

    //public AudioSource moveSoundEffect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator EnemyMove(){
        yield return StartCoroutine(EnemyMovementPhase()); 
    }

    IEnumerator EnemyMovementPhase()
    {

        //moveSoundEffect.Play();

        cam.Follow = rb.transform;

        float random = Random.Range(-movementValue, movementValue);

        while(-movementValue/4f < random && random < movementValue/4f){
            random = Random.Range(-movementValue, movementValue);
        }

        float newPos = rb.position.x + random;

        float timer = 0.0f;
        while (Mathf.Abs(rb.position.x - newPos) > 0.01f && timer < 3.0f){   
            float pos = rb.position.x;
            if(random < 0){
                rb.position = new Vector3(pos -= 0.01f, rb.position.y, 0);
            }else{
                rb.position = new Vector3(pos += 0.01f, rb.position.y, 0);
            }
    
            timer += Time.deltaTime; 
            yield return null;
        }

        rb.position = new Vector3(newPos, rb.position.y, 0);
        yield return new WaitForSeconds(1f);

        // Reset the camera to the main camera when the cannonball is destroyed
        if(player != null){
            cam.Follow = player.transform;
        }

        //moving = false;
    }
}