using UnityEngine;

public class WorldMapPlayer : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        if (xMove < 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }else if (xMove > 0){
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        if (yMove < 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }else if (yMove > 0){
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        transform.Translate(new Vector3(Time.deltaTime * speed * xMove, Time.deltaTime * speed * yMove, 0), Space.World);
    
    }
}