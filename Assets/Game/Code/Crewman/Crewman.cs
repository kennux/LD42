using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main object for crewman.
/// Provides a simple interface to the crewman behaviour model
/// </summary>
[RequireComponent(typeof(CrewmanModel), typeof(CrewmanHealthMechanic), typeof(CrewmanInteractionMechanic))]
[RequireComponent(typeof(CrewmanInteractionMechanic))]
public class Crewman : MonoBehaviour
{
    private void Start()
    {
        Game.instance.RegisterCrewman(this);
    }

    private void OnDestroy()
    {
        Game.instance.DeregisterCrewman(this);
    }
}
