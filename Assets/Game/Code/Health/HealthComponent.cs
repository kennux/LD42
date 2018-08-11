using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

public class HealthComponent : BehaviourModelMechanicComponent<HealthMechanic>
{
    [Header("Debug")]
    [SerializeField]
    private float health;

    private void OnTakeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.mechanic.die.Fire();
        }
    }

    protected override void BindHandlers()
    {
        this.health = this.mechanic.maxHealth.Get();
        this.mechanic.takeDamage.handler += OnTakeDamage;
    }
}
