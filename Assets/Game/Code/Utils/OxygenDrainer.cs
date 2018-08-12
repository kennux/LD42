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

    public void Update()
    {
        float dr = this.drainRate * Time.deltaTime;
        float d = Game.instance.ship.oxygen.GetDrainPercentage(dr);
        Game.instance.ship.oxygen.value -= d * dr;
    }
}
