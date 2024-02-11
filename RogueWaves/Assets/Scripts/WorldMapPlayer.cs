using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WorldMapPlayer : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] public GameObject level1;
    [SerializeField] public GameObject level2;
    [SerializeField] public GameObject level3;
    [SerializeField] public GameObject level4;
    [SerializeField] public GameObject level5;

    void Update()
    {
        Debug.Log("can move");
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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == level1) {
            SceneManager.LoadScene("Level");
        }else if (other.gameObject == level2) {
            SceneManager.LoadScene("Level2");
        } else if (other.gameObject == level3) {
            SceneManager.LoadScene("Level3");
        } else if (other.gameObject == level4) {
            SceneManager.LoadScene("Level4");
        } else if (other.gameObject == level5) {
            SceneManager.LoadScene("Level5");
        } else {
            Debug.Log("Rock");
        }
    }
}