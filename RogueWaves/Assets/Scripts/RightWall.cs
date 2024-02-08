using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other + "test");
        Destroy(other.gameObject);
    }
}
