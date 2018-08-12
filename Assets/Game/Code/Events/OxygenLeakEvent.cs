using UnityTK;
using UnityEngine;
using UnityTK.Audio;

[CreateAssetMenu(menuName = "LD42/Events/Oxygen Leak Event", fileName = "Oxygen Leak Event")]
public class OxygenLeakEvent : GameEvent
{
    public float oxygenPercentageLeaked;
    public AudioEvent nonSpatialAudio;

    public override void Execute()
    {
        Ship.instance.oxygen.value -= Ship.instance.oxygen.max * this.oxygenPercentageLeaked * (1f - Ship.instance.damageMitigation);
        AudioOneShotPlayer.instance.PlayNonSpatial(this.nonSpatialAudio);
    }

    public override float GetSpawnProbability()
    {
        return 1;
    }
}