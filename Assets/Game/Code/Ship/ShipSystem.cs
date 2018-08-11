using UnityEngine;
using UnityTK;

/// <summary>
/// Abstract base class for implementing ship systems.
/// </summary>
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