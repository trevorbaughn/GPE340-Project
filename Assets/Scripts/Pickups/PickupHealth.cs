using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickup
{
    [SerializeField] private float healthToHeal;

    public override void OnTriggerEnter(Collider other)
    {
        if (healthToHeal > 0)
        {
            Pawn thePawn = other.GetComponent<Pawn>();
            if (thePawn != null)
            {
                Health pawnHealth = thePawn.GetComponent<Health>();
                if (pawnHealth != null)
                {
                    pawnHealth.HealDamage(healthToHeal);
                }
            }
        }
        
        base.OnTriggerEnter(other);
    }
}