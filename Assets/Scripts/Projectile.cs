using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
{
    public float damage;
    public float moveSpeed;
    public float lifespan;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifespan); //destroy after lifespan over
    }

    public void Update()
    {
        //per second move forward (not per frame)
        rb.velocity = transform.forward * moveSpeed;       
    }

    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.GetComponent<Health>();
        if(otherHealth != null )
        {
            otherHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
