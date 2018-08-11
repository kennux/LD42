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
    public ModelActivity<MovementParameters> move = new ModelActivity<MovementParameters>();

    protected override void SetupConstraints()
    {

    }
}
