using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Private variables (not visible in the Inspector panel)
    // The speed of player movement
    float speed = 10;

    // Update is called once per frame
    void Update()
    {
        // Player movement from input (it's a variable between -1 and 1) for
        // degree of left or right movement
        float movementInput = Input.GetAxis("Horizontal");
        // Move the player object
        transform.Translate(new Vector3(Time.deltaTime * speed * movementInput, 0, 0), Space.World);
    }
}