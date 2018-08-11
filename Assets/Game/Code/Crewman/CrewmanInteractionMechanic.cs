using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

public class CrewmanInteractionMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// Starts interaction with the specified interactable.
    /// </summary>
    public ModelActivity<IInteractable> startInteraction = new ModelActivity<IInteractable>();

    protected override void SetupConstraints()
    {

    }
}
