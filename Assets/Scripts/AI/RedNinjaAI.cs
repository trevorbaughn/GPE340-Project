using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNinjaAI : AIController
{
    [SerializeField] private float shootingDistance;
    [SerializeField] private float shootingAngle;
    
    protected override void MakeDecisions()
    {
        if (controlledPawn == null) return;

        switch (currentState)
        {
            case AIStates.Idle:
                DoIdleState();

                if (IsTimePassed(0.25f))
                {
                    ChangeState(AIStates.SeekTarget);
                }
                break;
            case AIStates.ChaseAndPrimary:
                if (targetTransform == null) ChangeState(AIStates.Idle); //break before doing, because null check
                
                DoChaseAndPrimaryState();
                
                break;
            case AIStates.SeekTarget:
                SeekTarget();
                if (targetTransform != null)
                {
                    ChangeState(AIStates.ChaseAndPrimary);
                }
                break;
        }
    }

    private void DoChaseAndPrimaryState()
    {
        if (targetTransform == null) return;
        
        //chase target
        Chase(targetTransform);
        //rotate towards target
        controlledPawn.RotateToLookAt(targetTransform.position);
        
        //if in shooting distance
        if (DistanceToTarget <= shootingDistance)
        {
            //and within angle
            Vector3 vectorToTarget = targetTransform.position - controlledPawn.transform.position;
            if ( Vector3.Angle(controlledPawn.transform.forward, vectorToTarget) <= shootingAngle)
            {
                // pull the trigger
                controlledPawn.weapon.OnPrimaryAttackBegin.Invoke();
            }
        } else
        {
            // release the trigger if not in range
            controlledPawn.weapon.OnPrimaryAttackEnd.Invoke();
        }
    }
}
