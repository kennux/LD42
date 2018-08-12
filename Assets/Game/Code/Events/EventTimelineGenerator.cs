using UnityEngine;
using UnityTK;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LD42/Event Timeline Generator", fileName = "Event Timeline Generator")]
public class EventTimelineGenerator : ScriptableObject
{
    public float timelineLength = 1000000;

    [ContextMenu("Generate")]
    public void Generate()
    {
        foreach (var target in this.targets)
        {
            List<GeneratorRule> rules = new List<GeneratorRule>();
            List<EventTimeline.Entry> timeline = new List<EventTimeline.Entry>();

            float t = float.PositiveInfinity;
            foreach (var rule in this.rules)
            {
                if (t > rule.minTraveled)
                    t = rule.minTraveled;
            }

            while (t < timelineLength)
            {
                foreach (var rule in this.rules)
                {
                    if (t >= rule.minTraveled && t <= rule.maxTraveled)
                    {
                        rules.Add(rule);
                    }
                }

                var r = rules.RandomItem();
                timeline.Add(new EventTimeline.Entry()
                {
                    spawnTable = r.table,
                    traveledDistance = t
                });

                t += Random.Range(r.recoveryPeriodMin, r.recoveryPeriodMax);
                rules.Clear();
            }

            target.entries = timeline.ToArray();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(target);
#endif
        }
    }

    [System.Serializable]
    public class GeneratorRule
    {
        public EventSpawnTable table;
        public float recoveryPeriodMin;
        public float recoveryPeriodMax;
        public float minTraveled;
        public float maxTraveled = float.MaxValue;
    }

    public GeneratorRule[] rules;

    /// <summary>
    /// The target timeline where to write the generated results to.
    /// </summary>
    public EventTimeline[] targets;
}