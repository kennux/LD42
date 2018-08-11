using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;
using UnityEngine.AI;

public class CrewmanMovement : BehaviourModelMechanicComponent<CrewmanMovementMechanic>
{
    public NavMeshAgent agent;

    [Header("Debug")]
    [SerializeField]
    private bool isMoving;

    protected override void BindHandlers()
    {
        this.mechanic.move.RegisterStartCondition(CanStartMove);
        this.mechanic.move.RegisterStopCondition(CanStopMove);
        this.mechanic.move.onStart += OnMoveStart;
        this.mechanic.move.onStop += OnMoveStop;
    }

    private bool CanStartMove(Vector3 point)
    {
        return true; // this.agent.IsDone();
    }

    private bool CanStopMove()
    {
        return this.isMoving;
    }
    
    private void OnMoveStart(Vector3 point)
    {
        this.isMoving = true;
        this.agent.SetDestination(point);
    }

    private void OnMoveStop()
    {
        this.isMoving = false;
    }

    private void Update()
    {
        if (this.isMoving && this.agent.IsDone())
        {
            this.mechanic.move.TryStop();
        }
    }
}
