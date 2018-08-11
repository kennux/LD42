using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK;

/// <summary>
/// Main object for crewman.
/// Provides a simple interface to the crewman behaviour model
/// </summary>
[RequireComponent(typeof(CrewmanModel), typeof(HealthMechanic), typeof(CrewmanInteractionMechanic))]
[RequireComponent(typeof(CrewmanMovementMechanic), typeof(CrewmanExperienceMechanic))]
public class Crewman : MonoBehaviour
{
    public CrewmanModel model
    {
        get { return this._model.Get(this); }
    }
    private LazyLoadedComponentRef<CrewmanModel> _model = new LazyLoadedComponentRef<CrewmanModel>();

    private void Start()
    {
        Game.instance.crewmen.Add(this);
    }

    private void OnDestroy()
    {
        Game.instance.crewmen.Remove(this);
    }
}
