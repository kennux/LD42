using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTK.BehaviourModel;
using System.Threading.Tasks;

public class CrewmanDeathHandler : BehaviourModelMechanicComponent<HealthMechanic>
{
    public GameObject deadPrefab;
    public float destroyDeadAfter = 2f;

    protected override void BindHandlers()
    {
        this.mechanic.die.handler += OnDie;
    }

    private void OnDie()
    {
        var deadGo = Instantiate(deadPrefab, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);
        Destroy(deadGo, this.destroyDeadAfter);
    }
}
