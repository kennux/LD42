using UnityEngine;
using System.Collections.Generic;
using UnityTK;

[CreateAssetMenu(menuName = "LD42/Event Timeline", fileName = "Event Timeline")]
public class EventTimeline : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        /// <summary>
        /// The point in the traveled distance this entry will be executed.
        /// </summary>
        public float traveledDistance;

        public EventSpawnTable spawnTable;
    }
    public Entry[] entries;

    /// <summary>
    /// Gets events to spawn for the passed traveled distance.
    /// </summary>
    /// <param name="startDist">The traveled distance at the beginning of the frame</param>
    /// <param name="endDist">The traveled distance at the end of the frame</param>
    /// <param name="events">The events to spawn</param>
    public void GetEventsToSpawn(float startDist, float endDist, List<GameEvent> events)
    {
        foreach (var entry in this.entries)
        {
            if(startDist <= entry.traveledDistance && endDist >= entry.traveledDistance)
            {
                var evt = entry.spawnTable.SelectEvent();
                if (!ReferenceEquals(evt, null))
                    events.Add(evt);
            }
        }
    }
}