using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Private variables (not visible in the Inspector panel)
    // The speed of player movement
    float speed = 3;

    //bool hasMoved = false;

    // Reference to parallax game object
    public GameObject parallaxObj;

    // Refernce to the script component of parallax
    // game object
    private Parallax parallaxComp;

    public BattleSystem battleSystem;

    [SerializeField] private AudioSource creakSoundEffect;


    private void Start()
    {
        // Get the reference to the script component of
        // parallax game object
        parallaxComp = parallaxObj.GetComponent<Parallax>();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if it's the player's turn before moving
        if (battleSystem.state == BattleState.PlAYERMOVE)
        {
            HandleMovementInput();
        }

    }

    void HandleMovementInput(){

        // Player movement from input (it's a variable between -1 and 1) for
        // degree of left or right movement
        float movementInput = Input.GetAxis("Horizontal");
        // Move the player object

        //transform.Translate(new Vector3(Time.deltaTime * speed * movementInput, 0, 0), Space.World);

        // only move and play sound if inpuut exists
        if (Mathf.Abs(movementInput) > 0)
        {
            creakSoundEffect.Play();

            transform.Translate(new Vector3(Time.deltaTime * speed * movementInput, 0, 0), Space.World);
        }

        // Do the parallax shift            
        if (parallaxComp)
        {
            parallaxComp.Move(new Vector3(Time.deltaTime * speed * movementInput, 0, 0));
        }

    }
}