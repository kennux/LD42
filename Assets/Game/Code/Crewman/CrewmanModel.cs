using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK;
using UnityTK.BehaviourModel;

/// <summary>
/// The crewman behaviour model.
/// </summary>
public class CrewmanModel : BehaviourModel
{
    public HealthMechanic health
    {
        get { return this._health.Get(this); }
    }
    private LazyLoadedComponentRef<HealthMechanic> _health = new LazyLoadedComponentRef<HealthMechanic>();

    public CrewmanMovementMechanic movement
    {
        get { return this._commanding.Get(this); }
    }
    private LazyLoadedComponentRef<CrewmanMovementMechanic> _commanding = new LazyLoadedComponentRef<CrewmanMovementMechanic>();

    public CrewmanInteractionMechanic interact
    {
        get { return this._interact.Get(this); }
    }
    private LazyLoadedComponentRef<CrewmanInteractionMechanic> _interact = new LazyLoadedComponentRef<CrewmanInteractionMechanic>();
}
