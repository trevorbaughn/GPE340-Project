using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HumanoidPawn))]
public class Death_RagdollAndDestroy : GameAction
{
    private Health _health;
    private HumanoidPawn _humanoidPawn;
    public float destroyDelay;
    public UnityEvent onDestroyGameObject;

    private Collider _mainCollider;
    private Rigidbody _mainRigidbody;
    private Collider[] _childColliders;
    private Rigidbody[] _childRigidbodies;
    
    public override void Awake()
    {
        _health = GetComponent<Health>();
        _humanoidPawn = GetComponent<HumanoidPawn>();
        _mainRigidbody = GetComponent<Rigidbody>();
        _mainCollider = GetComponent<Collider>();
        _childRigidbodies = GetComponentsInChildren<Rigidbody>();
        _childColliders = GetComponentsInChildren<Collider>();

        base.Awake();
    }

    public override void Start()
    {
        if (_health != null) _health.OnDeath.AddListener(RagdollAndDestroy);

        DeactivateRagdoll();
        
        base.Start();
    }
    
    public void OnDestroy()
    {
        onDestroyGameObject.Invoke();
    }
    
    public void RagdollAndDestroy()
    {
        //unequip weapon, activate ragdoll, set timer for destruction
        _humanoidPawn.UnequipWeapon();
        ActivateRagdoll();
        Destroy(gameObject, destroyDelay);
    }
    
    private void ActivateRagdoll()
    {
        _humanoidPawn.GetAnimator().enabled = false;
        _humanoidPawn.pawnController.enabled = false;
        
        foreach (Collider collider in _childColliders)
        {
            collider.enabled = true;
        }
        foreach (Rigidbody rb in _childRigidbodies)
        {
            rb.isKinematic = false;
        }
        _mainCollider.enabled = false;
        _mainRigidbody.isKinematic = true;
    }

    private void DeactivateRagdoll()
    {
        _humanoidPawn.GetAnimator().enabled = true;
        _humanoidPawn.pawnController.enabled = true;

        
        foreach (Collider collider in _childColliders)
        {
            collider.enabled = false;
        }

        // isKinematic turns off rigidbodies
        foreach (Rigidbody rb in _childRigidbodies)
        {
            rb.isKinematic = true;
        }

        // re-enable collider and rigidbody
        _mainCollider.enabled = true;
        _mainRigidbody.isKinematic = false;
    }


    
    
}
