using UnityEngine;
using UnityTK;
using System.Collections.Generic;

/// <summary>
/// Game events system.
/// </summary>
public class GameEvents : SingletonBehaviour<GameEvents>
{
    /// <summary>
    /// Called when an event was executed.
    /// </summary>
    public event System.Action<GameEvent> onExecuteEvent;

    [Header("Config")]
    public EventTimeline[] easyTimelines;
    public EventTimeline[] mediumTimelines;
    public EventTimeline[] hardTimelines;
    public EventTimeline[] timelines;

    [Header("Debug")]
    public EventTimeline timeline;

    [SerializeField]
    private float startDist;

    public void Start()
    {
        switch (MainMenu.difficulty)
        {
            case GameDifficulty.EASY: this.timelines = this.easyTimelines; break;
            case GameDifficulty.MEDIUM: this.timelines = this.mediumTimelines; break;
            case GameDifficulty.HARD: this.timelines = this.hardTimelines; break;
        }
        MainMenu.difficulty = GameDifficulty.NULL;
        this.timeline = this.timelines.RandomItem();

        Game.instance.onPreTravel += OnPreTravel;
        Game.instance.onPostTravel += OnPostTravel;
    }

    private void OnPreTravel()
    {
        this.startDist = Game.instance.traveled;
    }

    private void OnPostTravel()
    {
        List<GameEvent> events = ListPool<GameEvent>.Get();

        this.timeline.GetEventsToSpawn(this.startDist, Game.instance.traveled, events);

        foreach (var evt in events)
        {
            Debug.Log("Executing event " + evt);
            this.onExecuteEvent?.Invoke(evt);
            evt.Execute();
        }

        ListPool<GameEvent>.Return(events);
    }
}