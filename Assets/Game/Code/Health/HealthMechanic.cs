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
    public ModelProperty<float> health;

    /// <summary>
    /// Event to be invoked when this object takes damage.
    /// </summary>
    public ModelEvent<float> takeDamage = new ModelEvent<float>();

    /// <summary>
    /// Fired when <see cref="health"/> reached 0 :<
    /// </summary>
    public ModelEvent die = new ModelEvent();

    protected override void SetupConstraints()
    {

    }
}
