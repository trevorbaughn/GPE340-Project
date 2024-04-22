using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class DeathDebugLog : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Health>().OnDeath.AddListener(OnDeathDebugLog);
    }

    void OnDeathDebugLog()
    {
        Debug.Log(this.name + " has died...");
        
    }
    
}