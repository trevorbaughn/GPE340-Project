using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private Collider pickupCollider;
    [SerializeField] private UnityEvent OnPickup;
    
    public virtual void Awake()
    {
        pickupCollider = GetComponent<Collider>();
        pickupCollider.isTrigger = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        OnPickup.Invoke();
        Destroy(this.gameObject);
    }
}
