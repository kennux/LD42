using UnityEngine;
using UnityTK;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UserInterface : SingletonBehaviour<UserInterface>
{
    public Ship ship
    {
        get { return Ship.instance; }
    }

    public float travelProgress
    {
        get { return Game.instance.travelProgress; }
    }

    public float invTravelProgress
    {
        get { return 1f - this.travelProgress; }
    }

    public float shipOxygenPercentage
    {
        get { return Game.instance.ship.oxygen.percentage; }
    }

    public float shipEnergyPercentage
    {
        get { return Game.instance.ship.energy.percentage; }
    }

    public float wallOfDeathProgress
    {
        get { return Game.instance.wallOfDeathProgress; }
    }

    public float invWallOfDeathProgress
    {
        get { return 1f - this.wallOfDeathProgress; }
    }

    public bool hasCrewmanSelected
    {
        get { return UISelection.instance.hasCrewmanSelected; }
    }

    public string shipVelocity
    {
        get { return "Ship Velocty: " + Ship.instance.velocity.value.ToString("0.00") + "  <color=\"black\">Edge of Space Velocity: " + Game.instance.wallOfDeathVelocity.ToString("0.00") + "</color>"; }
    }

    public bool isPaused { get { return Game.instance.isPaused; } }

    public CrewmanViewModel selectedCrewmanVM
    {
        get
        {
            var selected = UISelection.instance.selectedCrewman;
            if (!Essentials.UnityIsNull(selected))
            {
                foreach (var model in this.crewmanVMs)
                {
                    if (ReferenceEquals(model.crewman, selected))
                        return model;
                }
            }

            return null;
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public List<CrewmanViewModel> crewmanVMs = new List<CrewmanViewModel>();
    public List<ShipSystemViewModel> shipSystemVMs = new List<ShipSystemViewModel>();

    public override void Awake()
    {
        base.Awake();
        Game.instance.crewmen.onAdd += OnAddCrewman;
        Game.instance.crewmen.onRemove += OnRemoveCrewman;

        Ship.instance.systems.onAdd += OnAddShipSystem;
        Ship.instance.systems.onRemove += OnRemoveShipSystem;
    }

    private void OnAddShipSystem(ShipSystem system)
    {
        this.shipSystemVMs.Add(new ShipSystemViewModel(system));
    }

    private void OnRemoveShipSystem(ShipSystem system)
    {
        this.shipSystemVMs.RemoveAll((vm) => ReferenceEquals(vm.system, system));
    }

    private void OnAddCrewman(Crewman crewman)
    {
        this.crewmanVMs.Add(new CrewmanViewModel(crewman));
    }

    private void OnRemoveCrewman(Crewman crewman)
    {
        this.crewmanVMs.RemoveAll((vm) => ReferenceEquals(vm.crewman, crewman));
    }
}