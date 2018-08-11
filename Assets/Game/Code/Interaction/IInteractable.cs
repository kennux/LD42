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
}