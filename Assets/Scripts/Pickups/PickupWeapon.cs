using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : Pickup
{
    [SerializeField] private Weapon weaponToEquip;

    public override void OnTriggerEnter(Collider other)
    {
        if (weaponToEquip != null)
        {
            Pawn thePawn = other.GetComponent<Pawn>();
            if (thePawn != null)
            {
                thePawn.EquipWeapon(weaponToEquip);
            }
        }
        
        base.OnTriggerEnter(other);
    }
}
