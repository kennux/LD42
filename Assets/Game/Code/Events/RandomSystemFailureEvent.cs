using UnityTK;
using UnityEngine;
using UnityTK.Audio;

[CreateAssetMenu(menuName = "LD42/Events/Random System Failure Event", fileName = "Random System Failure Event")]
public class RandomSystemFailureEvent : GameEvent
{
    public AudioEvent nonSpatialAudio;
    
    public override void Execute()
    {
        var system = Ship.instance.systems[Random.Range(0, Ship.instance.systems.Count)];
        system.health.takeDamage.Fire(system.health.maxHealth.Get() * (1f - Ship.instance.damageMitigation));
        Debug.Log("Ship system " + system + " was affected by random system failure event!");
    }

    public override float GetSpawnProbability()
    {
        return 1;
    }
}