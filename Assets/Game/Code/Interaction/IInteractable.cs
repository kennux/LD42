using UnityEngine;
using UnityTK.BehaviourModel;

/// <summary>
/// Interface defining interactables.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// The interaction type of this interactable.
    /// </summary>
    InteractionType type { get; }

    /// <summary>
    /// The activity to be used for interactions.
    /// </summary>
    InteractionActivity interact { get; }

    /// <summary>
    /// The position from which the crewman are interacting with this interactable.
    /// </summary>
    Vector3 interactionPosition { get; }

    /// <summary>
    /// The quaternion look rotation for the interaction position in worldspace.
    /// </summary>
    Quaternion interactionLookRotation { get; }
}