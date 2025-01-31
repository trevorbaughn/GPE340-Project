using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator _animator;

    [SerializeField] protected Transform _weaponAttachmentPoint;
    [SerializeField] protected Weapon[] _startingWeaponOptions;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        _animator = GetComponent<Animator>();

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move(Vector3 direction)
    {
        if (_animator == null) return; 
        
        direction *= maxMovementSpeed;
        
        //convert from world space (controls) to local space for animator
        direction = transform.InverseTransformDirection(direction);
        
        _animator.SetFloat("Right", direction.x);
        _animator.SetFloat("Forward", direction.z);
    }

    public void SetCrouch(bool isCrouching)
    {
        _animator.SetBool("isCrouching", isCrouching);
    }

    public override void Rotate(float speed)
    {
        this.transform.Rotate(0, speed * maxRotationSpeed * Time.deltaTime, 0);
    }

    public override void RotateToLookAt(Vector3 target)
    {
        Vector3 lookVector = target - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);

        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }
    
    public Animator GetAnimator()
    {
        return _animator;
    }
    
    public void OnAnimatorMove()
    {
        transform.position = _animator.rootPosition;
        transform.rotation = _animator.rootRotation;
        
        AIController aiController = pawnController as AIController;
        if (aiController != null)
        {
            //set agent to understand next position as the position from _animator
            aiController.agent.nextPosition = _animator.rootPosition;
        }
    }
    
    public void OnAnimatorIK()
    {
        // no weapon, no need for IK
        if (!weapon) {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            return;
        }
        // set IK for right hand
        if (weapon.RightHandIKTarget)
        {
            _animator.SetIKPosition(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, weapon.RightHandIKTarget.rotation);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }
        // set IK for left hand
        if (weapon.LeftHandIKTarget)
        {
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, weapon.LeftHandIKTarget.rotation);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }

    public override void EquipWeapon(Weapon weaponToEquip)
    {
        UnequipWeapon();

        weapon = Instantiate(weaponToEquip, _weaponAttachmentPoint) as Weapon; //instantiate and add to player, parent is attachment point

        weapon.gameObject.layer = this.gameObject.layer;

        weapon.owner = this;
    }

    public override void UnequipWeapon()
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        weapon = null;
    }
}