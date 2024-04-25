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
    public bool isRagdoll;

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
        
        //activate ragdoll, set timer for destruction
        ActivateRagdoll();
        Destroy(gameObject, destroyDelay);
    }
    
    public void ActivateRagdoll()
    {
        
        _humanoidPawn.UnequipWeapon();
        _humanoidPawn.GetAnimator().enabled = false;
        if(_humanoidPawn.pawnController != null)
            _humanoidPawn.pawnController.enabled = false;
        
        
        foreach (Collider collider in _childColliders)
        {
            collider.enabled = true;
            collider.gameObject.layer = 8;
        }
        foreach (Rigidbody rb in _childRigidbodies)
        {
            rb.isKinematic = false;
            rb.gameObject.layer = 8;
        }
        _mainCollider.enabled = false;
        _mainRigidbody.isKinematic = true;
    }

    private void DeactivateRagdoll()
    {
        isRagdoll = false;
        _humanoidPawn.GetAnimator().enabled = true;
        if(_humanoidPawn.pawnController != null)
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
