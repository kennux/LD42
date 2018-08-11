using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

/// <summary>
/// Behaviour model mechanic handling crewman commanding and command execution.
/// Commands:
/// - Move to xy
/// - Start interacting with xy (repair / extinguish fire / man system)
/// </summary>
public class CrewmanCommandingMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// Commands this crewman to interace with an interactable.
    /// </summary>
    public ModelAttempt<IInteractable> commandInteracting = new ModelAttempt<IInteractable>();

    /// <summary>
    /// Commands the crewman to move to a specific point.
    /// </summary>
    public ModelAttempt<Vector3> commandMovement = new ModelAttempt<Vector3>();

    protected override void SetupConstraints()
    {

    }
}
