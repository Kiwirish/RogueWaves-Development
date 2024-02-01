using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour
{

    // define backGround size
    public float backgroundSize;

    public void Move(Vector3 cameraShift)
    {



        // Move the camera with the shifting
        // object
        Camera.main.transform.Translate(cameraShift);

        // For every child of the parallax game object
        // determine the degree of lag behind the camera
        // and adjust its shift
        foreach (Transform child in transform)
        {
            // Take the Z value to ind
            // the camera's shift
            // It is roughly rleated to distance.
            float keepUp = child.position.z;

            // vector to shift background
            Vector3 backgroundShift = cameraShift * keepUp;
            child.Translate(backgroundShift);

            //Check if the background needs to be moved to new position 
            if (child.position.x <= Camera.main.transform.position.x - backgroundSize / 2)
            {
                // reposition to the right
                Vector3 newPos = new Vector3(child.position.x + backgroundSize * 2, child.position.y, child.position.z);
                child.position = newPos;
            }
            else if (child.position.x >= Camera.main.transform.position.x + backgroundSize / 2)
            {
                //reposition to the left
                Vector3 newPos = new Vector3(child.position.x - backgroundSize * 2, child.position.y, child.position.z);
                child.position = newPos;
            }
        }
    }
}