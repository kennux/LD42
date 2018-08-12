using UnityEngine;
using UnityTK.BehaviourModel;
using System.Collections.Generic;
using UnityTK;

public class CrewmanExperience : BehaviourModelMechanicComponent<CrewmanExperienceMechanic>
{
    /// <summary>
    /// Experience for individual systems, 0-1 range.
    /// </summary>
    private Dictionary<ShipSystemType, float> experience = new Dictionary<ShipSystemType, float>();

    /// <summary>
    /// The rate at which units gain exp in exp/s.
    /// </summary>
    public float exGainRate = 0.0025f;

    public CrewmanInteractionMechanic interaction
    {
        get { return this._interaction.Get(this); }
    }
    private LazyLoadedComponentRef<CrewmanInteractionMechanic> _interaction = new LazyLoadedComponentRef<CrewmanInteractionMechanic>();

    private float GetExperience(ShipSystemType systemType)
    {
        float exp = 0;
        this.experience.TryGetValue(systemType, out exp);

        return exp;
    }

    private void OnInteractionTick(IInteractable interactable)
    {
        if (interactable is ShipSystem)
        {
            var ss = interactable as ShipSystem;
            AddExperience(ss.shipSystemType, this.exGainRate * Time.fixedDeltaTime);
        }
    }

    private void AddExperience(ShipSystemType systemType, float exp)
    {
        float current = this.experience.GetOrCreate(systemType);
        this.experience[systemType] = current + exp;
    }

    protected override void BindHandlers()
    {
        this.mechanic.getExperienceMultiplicator.BindHandler(this.GetExperience);
        this.interaction.interactionTick.handler += OnInteractionTick;
    }
}