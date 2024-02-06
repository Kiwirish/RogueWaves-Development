using UnityEngine;

public class WorldMapBoundary : MonoBehaviour
{
    public float boundaryRadius = 0.5f; // Set this to the radius of your circular boundary

    void Update()
    {
        // Check the distance from the center of the circular boundary
        float distance = Vector2.Distance(transform.position, Vector2.zero);

        // If the distance exceeds the boundary radius, move the object back inside
        if (distance > boundaryRadius)
        {
            Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;
            Vector2 newPosition = Vector2.zero - direction * boundaryRadius;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}