using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Controller : MonoBehaviour
{
    [HideInInspector] public Pawn controlledPawn;

    public float hitAccuracy;

    public UnityEvent OnPossess;

    
    protected virtual void Start()
    {
        Pawn pawnToPossess = GetComponent<Pawn>();
        if (pawnToPossess)
        {
            Possess(pawnToPossess);
        }
        
    }
    protected virtual void Update()
    {
        if (GameManager.instance.isPaused) return;
        
        MakeDecisions();
    }
    
    protected abstract void MakeDecisions();

    public virtual void Possess(Pawn pawnToPossess)
    {
        controlledPawn = pawnToPossess;

        controlledPawn.pawnController = this;
        
        controlledPawn.gameObject.layer = this.gameObject.layer; 
        
        OnPossess.Invoke();
    }

    public virtual void Unpossess()
    {
        controlledPawn.pawnController = null;
        
        controlledPawn = null;
    }
}