using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Pawn controlledPawn;

    
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
        MakeDecisions();
    }
    
    protected abstract void MakeDecisions();

    public virtual void Possess(Pawn pawnToPossess)
    {
        controlledPawn = pawnToPossess;

        controlledPawn.pawnController = this;
        
        controlledPawn.gameObject.layer = this.gameObject.layer; 
    }

    public virtual void Unpossess()
    {
        controlledPawn.pawnController = null;
        
        controlledPawn = null;
    }
}