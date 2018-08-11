using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTK.BehaviourModel;
using System.Threading.Tasks;

public class CrewmanDeathHandler : BehaviourModelMechanicComponent<HealthMechanic>
{
    protected override void BindHandlers()
    {
        this.mechanic.die.handler += OnDie;
    }

    private void OnDie()
    {
        // TODO: Animation
        Destroy(this.gameObject);
    }
}
