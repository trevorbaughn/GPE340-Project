using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move(Vector3 direction)
    {
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
}