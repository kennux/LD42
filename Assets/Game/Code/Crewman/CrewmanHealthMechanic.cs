using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

public class CrewmanHealthMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// The maximum health of this crewman.
    /// </summary>
    public ModelModifiableFloat maxHealth;

    /// <summary>
    /// The current health of this crewman.
    /// </summary>
    public ModelProperty<float> health;

    protected override void SetupConstraints()
    {

    }
}
