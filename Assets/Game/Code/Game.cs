using UnityEngine;
using UnityTK;
using System.Collections.Generic;

/// <summary>
/// Game manager singleton.
/// </summary>
public class Game : SingletonBehaviour<Game>
{
    [System.Serializable]
    public struct EventTimelineEntry
    {
        public double distance;
        public GameEvent evt;
    }

    [Header("Game Settings")]
    public double distanceToTravel = 1000000;
    public EventTimelineEntry[] eventTimeline;
    public Ship ship;

    [Header("Debug")]
    [SerializeField]
    private double traveled = 0;

    public ReadOnlyList<Crewman> crewmen
    {
        get
        {
            return new ReadOnlyList<Crewman>(this._crewmen);
        }
    }
    [SerializeField]
    private List<Crewman> _crewmen = new List<Crewman>();
    
    public void RegisterCrewman(Crewman crewman)
    {
        this._crewmen.Add(crewman);
    }

    public void DeregisterCrewman(Crewman crewman)
    {
        this._crewmen.Remove(crewman);
    }

    private void Update()
    {
        this.traveled += this.ship.velocity * Time.deltaTime;
        if (this.traveled >= this.distanceToTravel)
        {
            // You made it :>
            OnGameOver(true);
        }
    }

    private void OnGameOver(bool success)
    {

    }
}