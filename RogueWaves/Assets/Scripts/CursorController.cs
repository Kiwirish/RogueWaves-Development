using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D defaultCursor; // Assign in inspector
    public Texture2D clickCursor; // Assign in inspector

    private void Start()
    {
        // Set the default cursor on start
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        // On mouse down, change the cursor to the clickCursor
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Cursor.SetCursor(clickCursor, new Vector2(clickCursor.width / 2, clickCursor.height / 2), CursorMode.Auto);
        }

        // On mouse up, revert back to the default cursor
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}