using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK.BehaviourModel;

public class HealthComponent : BehaviourModelMechanicComponent<HealthMechanic>
{
    [Header("Config")]
    public bool canDie = true;

    [Header("Debug")]
    [SerializeField]
    private float health;

    [ContextMenu("Take 1 damage")]
    private void Take1Damage()
    {
        this.mechanic.takeDamage.Fire(1);
    }

    [ContextMenu("Fully heal")]
    private void FullyHeal()
    {
        this.mechanic.heal.Fire(this.mechanic.maxHealth.Get() - this.health);
    }

    private void OnTakeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            if (canDie)
                this.mechanic.die.Fire();
            else
                this.health = 0;
        }
    }

    private void OnHeal(float health)
    {
        bool wasNotFull = !Mathf.Approximately(this.health, this.mechanic.maxHealth.Get());
        this.health = Mathf.Clamp(this.health + health, 0, this.mechanic.maxHealth.Get());

        if (wasNotFull && Mathf.Approximately(this.health, this.mechanic.maxHealth.Get()))
        {
            this.mechanic.fullyHealed.Fire();
        }
    }

    private float GetHealth()
    {
        return this.health;
    }

    protected override void BindHandlers()
    {
        this.mechanic.health.SetGetter(this.GetHealth);
        this.health = this.mechanic.maxHealth.Get();
        this.mechanic.takeDamage.handler += OnTakeDamage;
        this.mechanic.heal.handler += OnHeal;
    }
}
