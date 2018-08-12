using UnityTK;
using UnityEngine;
using UnityTK.Audio;

[CreateAssetMenu(menuName = "LD42/Events/Gravity Well Event", fileName = "Gravity Well Event")]
public class GravityWellEvent : GameEvent
{
    public float velocityRemove;
    public AudioEvent nonSpatialAudio;

    public override void Execute()
    {
        Ship.instance.velocity.value -= this.velocityRemove * (1f - Ship.instance.damageMitigation);
        AudioOneShotPlayer.instance.PlayNonSpatial(this.nonSpatialAudio);
    }

    public override float GetSpawnProbability()
    {
        return 1;
    }
}