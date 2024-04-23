using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //gizmo size
    private float gizmoBoxHeight = 2;
    private float gizmoBoxWidth = 1; 

    public void OnDrawGizmos()
    {
        //set color
        Color boxColor = Color.yellow;
        boxColor.a = 0.7f;
        Gizmos.color = boxColor;

        // since box pos is at center, draw half it's height up
        Vector3 boxPosition = transform.position;
        boxPosition += Vector3.up * (gizmoBoxHeight / 2);
        
        //draw box
        Vector3 boxSize = new Vector3(gizmoBoxWidth, gizmoBoxHeight, gizmoBoxWidth);
        Gizmos.DrawCube(boxPosition, boxSize);

        // draw ray
        Gizmos.color = Color.red;
        Gizmos.DrawRay(boxPosition, transform.forward);
    }
}
