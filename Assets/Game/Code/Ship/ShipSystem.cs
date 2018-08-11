using UnityEngine;
using UnityTK;

/// <summary>
/// Abstract base class for implementing ship systems.
/// </summary>
[RequireComponent(typeof(ShipSystemModel), typeof(HealthMechanic))]
public abstract class ShipSystem : MonoBehaviour, IInteractable
{
    public InteractionType type
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public InteractionActivity interact
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
    protected InteractionActivity _interact;


}