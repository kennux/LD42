using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;
using UnityEngine.AI;

public class CrewmanMovement : BehaviourModelMechanicComponent<CrewmanMovementMechanic>
{
    [Header("Config")]
    public float turnDist = 1;
    public NavMeshAgent agent;

    [Header("Debug")]
    [SerializeField]
    private bool isMoving;
    private Quaternion? lookRotation;

    protected override void BindHandlers()
    {
        this.mechanic.move.RegisterStartCondition(CanStartMove);
        this.mechanic.move.RegisterStopCondition(CanStopMove);
        this.mechanic.move.onStart += OnMoveStart;
        this.mechanic.move.onStop += OnMoveStop;
    }

    private bool CanStartMove(MovementParameters point)
    {
        return true; // this.agent.IsDone();
    }

    private bool CanStopMove()
    {
        return this.isMoving;
    }
    
    private void OnMoveStart(MovementParameters point)
    {
        this.isMoving = true;
        this.lookRotation = point.lookRotation;
        this.agent.updateRotation = true;
        this.agent.SetDestination(point.position);
    }

    private void OnMoveStop()
    {
        this.isMoving = false;
    }

    private void Update()
    {
        if (this.isMoving)
        {
            bool isDone = this.agent.IsDone();
            if (isDone)
                this.mechanic.move.TryStop();
            else if (this.lookRotation.HasValue)
            {
                float dist = (this.transform.position - this.agent.pathEndPosition).magnitude;
                if (dist < this.turnDist)
                {
                    this.agent.updateRotation = false;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.lookRotation.Value, 1f - (dist / this.turnDist));
                }
                else
                    this.agent.updateRotation = true;
            }
            else if (!this.lookRotation.HasValue)
            {
                this.agent.updateRotation = true;
            }
        }
    }
}
