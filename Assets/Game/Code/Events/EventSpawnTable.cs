using UnityEngine;
using System.Collections.Generic;
using UnityTK;

[CreateAssetMenu(menuName = "LD42/Event Spawn Table", fileName = "Event Spawn Table")]
public class EventSpawnTable : ScriptableObject
{
    [System.Serializable]
    public struct EventSpawn
    {
        public GameEvent evt;
        public float probability;
    }

    public EventSpawn[] entries;

    public GameEvent SelectEvent()
    {
        return SelectEvent(entries);
    }

    private GameEvent SelectEvent(EventSpawn[] events)
    {
        // Build probability table
        List<EventSpawn> probabilityTable = ListPool<EventSpawn>.Get();
        float probabilityOverall = 0;

        // Parse
        foreach (var evt in events)
        {
            float p = evt.probability * evt.evt.GetSpawnProbability();
            probabilityOverall += p;
            probabilityTable.Add(new EventSpawn()
            {
                evt = evt.evt,
                probability = p,
            });
        }

        // Normalize
        for (int i = 0; i < probabilityTable.Count; i++)
        {
            var e = probabilityTable[i];
            e.probability /= probabilityOverall;
            probabilityTable[i] = e;
        }

        // Select
        float rnd = Random.value;
        for (int i = 0; i < probabilityTable.Count; i++)
        {
            var e = probabilityTable[i];
            float startProbability = (i == 0) ? 0 : e.probability;
            float endProbability = (i == probabilityTable.Count - 1) ? 1 : probabilityTable[i + 1].probability;

            if (rnd >= startProbability && rnd <= endProbability)
                return e.evt;
        }


        // Wtf?!
        Debug.LogError("Event selection algorithm couldnt select an event!?");
        return null;
    }
}