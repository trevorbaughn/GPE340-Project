using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;
    public Transform targetTransform;
    private Vector3 _desiredVelocity = Vector3.zero;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Possess(Pawn pawnToPossess)
    {
        base.Possess(pawnToPossess);

        //Add navmesh agent, and create one if needed
        agent = controlledPawn.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = controlledPawn.AddComponent<NavMeshAgent>();
        }

        //set agent variables
        agent.stoppingDistance = stoppingDistance;
        agent.speed = controlledPawn.maxMovementSpeed;
        agent.angularSpeed = controlledPawn.maxRotationSpeed;

        //disable movement and rotation from agent
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public override void Unpossess()
    {
        //no need for navmesh if not possessed
        Destroy(agent);
        
        base.Unpossess();
    }

    protected override void MakeDecisions()
    {
        if (controlledPawn == null)
        {
            return;
        }

        agent.SetDestination(targetTransform.position);

        //find agent's desired velocity
        _desiredVelocity = agent.desiredVelocity;
        
        //move it
        controlledPawn.Move(_desiredVelocity.normalized);
        
        //rotate towards target
        controlledPawn.RotateToLookAt(targetTransform.position);
    }
}
