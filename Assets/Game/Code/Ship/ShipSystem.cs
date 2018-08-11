using UnityEngine;
using UnityTK;

/// <summary>
/// Abstract base class for implementing ship systems.
/// </summary>
[RequireComponent(typeof(ShipSystemModel), typeof(HealthMechanic))]
public abstract class ShipSystem : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The position from which to interact.
    /// </summary>
    public Transform interactionAnchor;

    public InteractionType type
    {
        get
        {
            return InteractionType.MAN_STATION;
        }
    }

    public InteractionActivity interact
    {
        get
        {
            return _interact;
        }
    }

    public Vector3 interactionPosition
    {
        get
        {
            return interactionAnchor.position;
        }
    }

    protected InteractionActivity _interact = new InteractionActivity();


}