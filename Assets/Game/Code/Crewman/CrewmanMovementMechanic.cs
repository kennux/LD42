using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

/// <summary>
/// 
/// </summary>
public class CrewmanMovementMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// Commands the crewman to move to a specific point.
    /// </summary>
    public ModelActivity<Vector3> move = new ModelActivity<Vector3>();

    protected override void SetupConstraints()
    {

    }
}
