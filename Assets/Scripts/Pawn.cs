using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Pawn : MonoBehaviour
{
    public Controller pawnController;

    public float maxMovementSpeed = 10.0f;
    public float maxRotationSpeed = 10.0f;

    public Weapon weapon;
    [SerializeField] protected Weapon[] startingWeaponOptions;
    
    protected virtual void Start()
    {
        if (startingWeaponOptions.Length > 0)
        {
            EquipWeapon(startingWeaponOptions[UnityEngine.Random.Range(0,startingWeaponOptions.Length)]);
        }
    }
    
    public abstract void Move(Vector3 moveDir);
    public abstract void Rotate(float speed);

    public abstract void RotateToLookAt(Vector3 target);

    public abstract void EquipWeapon(Weapon weaponToEquip);

    public abstract void UnequipWeapon();

    
}
