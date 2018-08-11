using UnityEngine;
using UnityTK;

public class Ship : SingletonBehaviour<Ship>
{
    public MinMaxResoruce velocity;
    public MinMaxResoruce energy;
    public MinMaxResoruce oxygen;

    public ObservableList<ShipSystem> systems = new ObservableList<ShipSystem>(new System.Collections.Generic.List<ShipSystem>());

    public override void Awake()
    {
        base.Awake();

        this.velocity.Init();
        this.energy.Init();
        this.oxygen.Init();
    }
}