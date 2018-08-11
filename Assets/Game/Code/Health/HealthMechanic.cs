using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

public class HealthMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// The maximum health of this crewman.
    /// </summary>
    public ModelModifiableFloat maxHealth;

    /// <summary>
    /// The current health of this crewman.
    /// </summary>
    public ModelProperty<float> health = new ModelProperty<float>();

    /// <summary>
    /// Event to be invoked when this object takes damage.
    /// </summary>
    public ModelEvent<float> takeDamage = new ModelEvent<float>();

    /// <summary>
    /// Event to be invoked when this object is healed.
    /// </summary>
    public ModelEvent<float> heal = new ModelEvent<float>();

    /// <summary>
    /// Invoked from health component when this mechanic was fully healed.
    /// </summary>
    public ModelEvent fullyHealed = new ModelEvent();

    /// <summary>
    /// Fired when <see cref="health"/> reached 0 :<
    /// </summary>
    public ModelEvent die = new ModelEvent();

    protected override void SetupConstraints()
    {

    }
}
