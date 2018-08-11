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

    public float travelProgress
    {
        get { return (float)(this.traveled / this.distanceToTravel); }
    }

    public float wallOfDeathProgress
    {
        get { return (float)(this.wallOfDeath / this.distanceToTravel); }
    }

    [Header("Game Settings")]
    public double distanceToTravel = 1000000;
    public EventTimelineEntry[] eventTimeline;
    public Ship ship;
    public double wallOfDeathAcceleration = 10f;
    public GameObject uiGameOverFail;
    public GameObject uiGameOverSuccess;

    [Header("Debug")]
    [SerializeField]
    private double traveled = 0;
    [SerializeField]
    private double wallOfDeath = 0;
    [SerializeField]
    private double wallOfDeathVelocity = 0;

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
        this.wallOfDeathVelocity += this.wallOfDeathAcceleration * Time.deltaTime;
        this.wallOfDeath += this.wallOfDeathVelocity * Time.deltaTime;
        this.traveled += this.ship.velocity * Time.deltaTime;
        if (this.traveled >= this.distanceToTravel)
        {
            // You made it :>
            OnGameOver(true);
        }
        else if (this.wallOfDeath >= this.traveled)
        {
            // You tried, mate :<
            OnGameOver(false);
        }
        else if (this.crewmen.count == 0)
        {
            // Well...
            OnGameOver(false);
        }
    }

    private void OnGameOver(bool success)
    {
        (success ? this.uiGameOverSuccess : this.uiGameOverFail).SetActive(true);
    }
}