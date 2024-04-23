using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
            MoveCamera(target, distance, speed);
    }

    private void MoveCamera(Transform target, float distance, float speed)
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + distance, target.position.z);

        this.transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        
        transform.LookAt(new Vector3(transform.position.x, 0, transform.position.z), transform.up);
        
    }
}
