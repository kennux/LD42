using UnityTK;
using UnityEngine;
using UnityTK.Audio;

[CreateAssetMenu(menuName = "LD42/Events/Solar Flare Event", fileName = "Solar Flare Event")]
public class SolarFlareEvent : GameEvent
{
    public AudioEvent nonSpatialAudio;
    
    public override void Execute()
    {
        Ship.instance.energy.value = 0;
        AudioOneShotPlayer.instance.PlayNonSpatial(this.nonSpatialAudio);
    }

    public override float GetSpawnProbability()
    {
        return Ship.instance.oxygen.percentage;
    }
}