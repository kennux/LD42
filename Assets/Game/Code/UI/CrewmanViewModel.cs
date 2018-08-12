using UnityEngine;

/// <summary>
/// UI view model for crewmen.
/// </summary>
public class CrewmanViewModel
{
    public Crewman crewman;

    public string crewmanName { get { return this.crewman.gameObject.name; } }
    public float o2GeneratorExp { get { return this.crewman.model.exp.getExperienceMultiplicator.Invoke(ShipSystemType.O2_GENERATOR); } }
    public float medbayExp { get { return this.crewman.model.exp.getExperienceMultiplicator.Invoke(ShipSystemType.MEDBAY); } }
    public float lifeSupportExp { get { return this.crewman.model.exp.getExperienceMultiplicator.Invoke(ShipSystemType.SHIELD); } }
    public float cockpitExp { get { return this.crewman.model.exp.getExperienceMultiplicator.Invoke(ShipSystemType.COCKPIT); } }
    public float thrusterExp { get { return this.crewman.model.exp.getExperienceMultiplicator.Invoke(ShipSystemType.THRUSTER); } }
    public float powerGeneratorExp { get { return this.crewman.model.exp.getExperienceMultiplicator.Invoke(ShipSystemType.POWER_GENERATOR); } }

    public CrewmanViewModel(Crewman crewman)
    {
        this.crewman = crewman;
    }
}