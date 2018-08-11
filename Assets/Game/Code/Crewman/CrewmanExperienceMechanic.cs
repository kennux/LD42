using UnityEngine;
using UnityTK;
using UnityTK.BehaviourModel;

public class CrewmanExperienceMechanic : BehaviourModelMechanic
{
    /// <summary>
    /// Gets the efficiency add multiplicator of this crewman for the specified system type.
    /// <see cref="ShipSystem.efficiencyMannedAdd"/>, range 0-1
    /// </summary>
    public ModelFunction<ShipSystemType, float> getExperienceMultiplicator = new ModelFunction<ShipSystemType, float>();

    protected override void SetupConstraints()
    {

    }
}