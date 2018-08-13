using UnityEngine;
using UnityTK;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LD42/Events/Asteroid Event", fileName = "Asteroid Event")]
public class AsteroidEvent : GameEvent
{
    public int asteroidsToFire;
    public float interval = 1.5f;
    public Asteroid prefab;
    
    public override void Execute()
    {
        // Find targets
        HashSet<ShipSystem> targets = HashSetPool<ShipSystem>.Get();

        var systems = Ship.instance.systems;
        while (targets.Count < this.asteroidsToFire)
            targets.Add(systems[Random.Range(0, systems.Count)]);

        // Fire asteroids
        float t = 0;
        foreach (var target in targets)
        {
            var asteroidGo = Instantiate(this.prefab.gameObject);
            var asteroid = asteroidGo.GetComponent<Asteroid>();
            asteroid.Initialize(target.gameObject, t);
            t += this.interval;
        }

        HashSetPool<ShipSystem>.Return(targets);
    }

    public override float GetSpawnProbability()
    {
        return 1;
    }
}