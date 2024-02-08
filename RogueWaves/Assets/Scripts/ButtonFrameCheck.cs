using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFrameCheck : MonoBehaviour
{

    private Collider2D collider;
    private Canvas canvas;

    public bool inFrame = false;
    
    void OnMouseOver(){
        inFrame = true;
    }

    void OnMouseExit(){
        inFrame = false; 
    }
}
