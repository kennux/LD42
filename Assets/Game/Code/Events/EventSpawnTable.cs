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
        if (events.Length == 1)
            return events[0].evt;

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
        float n = 0;
        for (int i = 0; i < probabilityTable.Count; i++)
        {
            var e = probabilityTable[i];
            e.probability /= probabilityOverall;
            n += e.probability;
            e.probability += n;
            probabilityTable[i] = e;
        }

        // Select
        float rnd = Random.value;
        for (int i = 0; i < probabilityTable.Count; i++)
        {
            var e = probabilityTable[i];

            float startProbability = 0, endProbability = 0;
            if (i == 0)
            {
                // First element!
                startProbability = 0;
                endProbability = probabilityTable[i].probability;
            }
            else if (i == probabilityTable.Count - 1)
            {
                // Last element
                startProbability = probabilityTable[i].probability;
                endProbability = 1;
            }
            else
            {
                // Middle element
                startProbability = probabilityTable[i-1].probability;
                endProbability = probabilityTable[i].probability;
            }

            if (rnd >= startProbability && rnd <= endProbability)
                return e.evt;
        }


        // Wtf?!
        Debug.LogError("Event selection algorithm couldnt select an event on table " + this);
        for (int i = 0; i < probabilityTable.Count; i++)
        {
            var e = probabilityTable[i];

            float startProbability = 0, endProbability = 0;
            if (i == 0)
            {
                // First element!
                startProbability = 0;
                endProbability = probabilityTable[i].probability;
            }
            else if (i == probabilityTable.Count - 1)
            {
                // Last element
                startProbability = probabilityTable[i].probability;
                endProbability = 1;
            }
            else
            {
                // Middle element
                startProbability = probabilityTable[i - 1].probability;
                endProbability = probabilityTable[i].probability;
            }

            if (rnd >= startProbability && rnd <= endProbability)
                return e.evt;
        }
        return null;
    }
}