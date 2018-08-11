using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;
using UnityEngine.AI;

public class CrewmanMovement : BehaviourModelMechanicComponent<CrewmanMovementMechanic>
{
    public NavMeshAgent agent;

    protected override void BindHandlers()
    {
        this.mechanic.move.RegisterCondition(CanMove);
        this.mechanic.move.onFire += OnCommandMove;
    }

    private bool CanMove(Vector3 point)
    {
        return true; // this.agent.IsDone();
    }
    
    private void OnCommandMove(Vector3 point)
    {
        this.agent.SetDestination(point);
    }
}
