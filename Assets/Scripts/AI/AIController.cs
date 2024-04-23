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
    protected Vector3 DesiredVelocity = Vector3.zero;
    
    public enum AIStates { Idle, Chase, ChaseAndPrimary};
    [Tooltip("The state the AI will start on")]
    [SerializeField] protected AIStates currentState;
    protected float timeEnteredCurrentState;
    
    protected float DistanceToTarget;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        //starting state
        ChangeState(currentState);
    }
    
    public virtual void ChangeState(AIStates newState)
    {
        //remember change state time
        timeEnteredCurrentState = Time.time;
        //change state
        currentState = newState;
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

        float distanceToTarget = Vector3.Distance(targetTransform.position, controlledPawn.transform.position);
    }
    
    /// <summary>
    /// Helper for AI break statemenbts
    /// </summary>
    /// <param name="amountOfTime"></param>
    /// <returns></returns>
    public virtual bool IsTimePassed (float amountOfTime)
    {
        if (Time.time - timeEnteredCurrentState >= amountOfTime)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Idle State everything uses as a base
    /// </summary>
    protected virtual void DoIdleState()
    {
        //do nothing
    }

    protected virtual void Chase(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);

        //find agent's desired velocity
        DesiredVelocity = agent.desiredVelocity;
        
        //move it
        controlledPawn.Move(DesiredVelocity.normalized);
    }
    
    //function override for if programmer wants to use Transform
    protected virtual void Chase(Transform target)
    {
        Chase(target.position);
    }
}
