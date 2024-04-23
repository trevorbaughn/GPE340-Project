using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowNinjaAI : AIController
{
    [SerializeField] private float shootingDistance;
    [SerializeField] private float shootingAngle;

    protected override void Start()
    {
        base.Start();

        //set actual stopping distance to close to 0, since we're manually doing that to target behind target
        agent.stoppingDistance = 0.5f;
    }
    
    protected override void MakeDecisions()
    {
        

        switch (currentState)
        {
            case AIStates.Idle:
                DoIdleState();
                
                if (IsTimePassed(0.25f))
                {
                    ChangeState(AIStates.ChaseAndPrimary);
                }
                break;
            case AIStates.ChaseAndPrimary:
                if (targetTransform == null) ChangeState(AIStates.Idle); //break before doing, because null check
                
                DoChaseAndPrimaryState();
                break;
            
        }
        
        
    }
    
    private void DoChaseAndPrimaryState()
    {
        //target behind target
        Vector3 targetMovePos = targetTransform.position - targetTransform.forward * Mathf.Sqrt(stoppingDistance);
        

        //chase target
        Chase(targetMovePos);
        controlledPawn.RotateToLookAt(targetTransform.position);
        
        //if in shooting distance
        if (Vector3.Distance(targetTransform.position, controlledPawn.transform.position) <= shootingDistance)
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
