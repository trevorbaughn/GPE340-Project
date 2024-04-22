using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] 
public class LaserBeam : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Color color = Color.white;
    public float lifespan = 0.1f;
    public float width = 0.5f;
    private LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    
        // Set vars
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;    

        // Set the start and end points
        Vector3[] points = {startPoint, endPoint};
        lineRenderer.SetPositions(points);

        // Self-destruct after lifespan
        Destroy(gameObject, lifespan );        
    }
}