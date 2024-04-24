using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathDestroy : GameAction
{
    public override void Awake()
    {
        GetComponent<Health>().OnDeath.AddListener(OnDeathDestroy);
        
        base.Awake();
    }

    private void OnDeathDestroy()
    {
        Destroy(this.gameObject);
    }
    
}
