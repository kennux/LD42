using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

public class CrewmanInteractionMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// Commanding interaction activity.
    /// This activity can be used to tell the crewman to interact with something no matter where its located atm.
    /// This will make the crewman go there and as soon as he arrived interaction starts.
    /// </summary>
    public ModelActivity<IInteractable> commandInteract = new ModelActivity<IInteractable>();

    /// <summary>
    /// Starts interaction with the specified interactable.
    /// Note that this starts interaction immediately!
    /// </summary>
    public ModelActivity<IInteractable> interact = new ModelActivity<IInteractable>();

    /// <summary>
    /// Fired every frame in which the crewman is interacting with something from Update().
    /// </summary>
    public ModelEvent<IInteractable> interactionTick = new ModelEvent<IInteractable>();

    protected override void SetupConstraints()
    {

    }
}
