using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour for things that drain oxygen
/// </summary>
public class OxygenDrainer : MonoBehaviour
{
    /// <summary>
    /// The rate at which to drain in units / second.
    /// </summary>
    public float drainRate = 0.1f;

    /// <summary>
    /// The amount of damage to take when no oxygen is available per second.
    /// </summary>
    public float damageWhenNoOxygenAvailable;

    public void Update()
    {
        float dr = this.drainRate * Time.deltaTime;
        float d = Game.instance.ship.oxygen.GetDrainPercentage(dr);
        Game.instance.ship.oxygen.value -= d * dr;

        if (damageWhenNoOxygenAvailable > 0)
        {
            float invD = 1f - d;
            if (invD > 0)
            {
                this.GetComponent<HealthMechanic>().takeDamage.Fire(damageWhenNoOxygenAvailable * invD * Time.deltaTime);
            }
        }
    }
}
