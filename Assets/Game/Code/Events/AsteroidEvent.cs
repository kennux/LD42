using UnityEngine;
using UnityTK;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LD42/Events/Asteroid Event", fileName = "Asteroid Event")]
public class AsteroidEvent : GameEvent
{
    public int asteroidsToFire;
    public Asteroid prefab;
    
    public override void Execute()
    {
        // Find targets
        HashSet<ShipSystem> targets = HashSetPool<ShipSystem>.Get();

        var systems = Ship.instance.systems;
        while (targets.Count < this.asteroidsToFire)
            targets.Add(systems[Random.Range(0, systems.Count)]);
        
        // Fire asteroids
        foreach (var target in targets)
        {
            var asteroidGo = Instantiate(this.prefab.gameObject);
            var asteroid = asteroidGo.GetComponent<Asteroid>();
            asteroid.Initialize(target.gameObject);
        }

        HashSetPool<ShipSystem>.Return(targets);
    }

    public override float GetSpawnProbability()
    {
        return 1;
    }
}