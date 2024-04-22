using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathDestroy : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Health>().OnDeath.AddListener(OnDeathDestroy);
    }

    void OnDeathDestroy()
    {
        Destroy(this.gameObject);
    }
    
}
