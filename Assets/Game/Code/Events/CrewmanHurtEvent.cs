using UnityTK;
using UnityEngine;
using UnityTK.Audio;

[CreateAssetMenu(menuName = "LD42/Events/Crewman Hurt Event", fileName = "Crewman Hurt Event")]
public class CrewmanHurtEvent : GameEvent
{
    public float damageToDeal;
    public AudioEvent nonSpatialAudio;

    public override void Execute()
    {
        var crewman = Game.instance.crewmen[Random.Range(0, Game.instance.crewmen.Count)];
        crewman.model.health.takeDamage.Fire(this.damageToDeal);

        AudioOneShotPlayer.instance.PlayNonSpatial(this.nonSpatialAudio);
    }

    public override float GetSpawnProbability()
    {
        return 1;
    }
}