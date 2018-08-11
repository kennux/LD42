using UnityEngine;

/// <summary>
/// UI view model for ship systems.
/// </summary>
public class ShipSystemViewModel
{
    public ShipSystem system;

    public ShipSystemViewModel(ShipSystem system)
    {
        this.system = system;
    }
}