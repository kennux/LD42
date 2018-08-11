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
    public float distanceToTravel = 1000000;
    public EventTimelineEntry[] eventTimeline;
    public Ship ship;
    public float wallOfDeathAcceleration = 10f;
    public float wallOfDeathMaxVelocity = 2500f;
    public GameObject uiGameOverFail;
    public GameObject uiGameOverSuccess;

    [Header("Debug")]
    [SerializeField]
    private float traveled = 0;
    [SerializeField]
    private float wallOfDeath = 0;
    [SerializeField]
    private float wallOfDeathVelocity = 0;
    public float playTime;
    
    public ObservableList<Crewman> crewmen = new ObservableList<Crewman>(new List<Crewman>());

    public override void Awake()
    {
        base.Awake();
        System.GC.Collect(); // Collect once initially
    }

    private void Update()
    {
        this.playTime += Time.deltaTime / Time.timeScale;
        this.wallOfDeathVelocity = Mathf.Clamp(this.wallOfDeathVelocity + this.wallOfDeathAcceleration * Time.deltaTime, 0, this.wallOfDeathMaxVelocity);
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
        else if (this.crewmen.Count == 0)
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