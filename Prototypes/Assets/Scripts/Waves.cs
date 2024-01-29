using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Waves : MonoBehaviour {
   
   // How dramatic the waves will appear
   public float waveIntensity;

   // Reference to line renderer component
   private LineRenderer   lr;

   //Array of point positions
   private Vector3[] linePoints;

   // Reference to the edge collider component
   private EdgeCollider2D ec;
   // Array of edge collider points
   private Vector2[] edgePoints; 

   // Use this for initialization
   void Start () {
      // Fetch the reference to the line renderer component
      lr = GetComponent<LineRenderer>();

      // Get the number of line points
      int numLinePoints = lr.positionCount;
      // Initialise the points position array
      linePoints = new Vector3[numLinePoints];

      // Fetch the reference to the edge collider component
      ec = GetComponent<EdgeCollider2D>();
      // Initialise the edge points array
      edgePoints = new Vector2[numLinePoints];
   }

   // Update is called once per frame
   void Update () {

      // Get the number of line points
      int numLinePoints = lr.positionCount;
      // Left-most and right-most points (in local coordinates),
      // between which line is rendered
      float xLeft = -10f;
      float xRight = 10f;

      //Distance between points on the line (calculated to fit exactly
      //numPoints from xLeft to xRight)
      float dx = (xRight-xLeft)/(float) (numLinePoints-1);
      for(int i=0;i<numLinePoints;i++) {
         //Horizontal location of point i
         linePoints[i].x = xLeft+i*dx;
         //Vertical location of point i is changing with time
         linePoints[i].y = waveIntensity*Mathf.Sin(linePoints[i].x/20f+Time.timeSinceLevelLoad);
         // Z-coordinate of point i (taken from game object's
         // z coordinate)
         linePoints[i].z = transform.position.z;

         //Horizontal location of edge point i follows the line
         edgePoints[i].x = linePoints[i].x;
         //Vertical location of edge point i follows the line
         edgePoints[i].y = linePoints[i].y;

      }

      // Set the line renderer points to the positions from point array
      lr.SetPositions(linePoints);

      // Set the edge point to the poisitions from edge point array
      ec.points = edgePoints;
   }
}
