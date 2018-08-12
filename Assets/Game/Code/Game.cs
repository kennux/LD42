using UnityEngine;
using UnityTK;
using System.Collections.Generic;

/// <summary>
/// Game manager singleton.
/// </summary>
public class Game : SingletonBehaviour<Game>
{
    public event System.Action onPreTravel;
    public event System.Action onPostTravel;

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
    public Ship ship;
    public AnimationCurve wallOfDeathSpeedup;

    public float wallOfDeathMaxVelocity;
    public float wallOfDeathMinVelocity;
    public float wallOfDeathDistanceMaxVelocity;

    public GameObject uiGameOverFail;
    public GameObject uiGameOverSuccess;

    [Header("Debug")]
    public float traveled = 0;
    public float wallOfDeath = 0;
    public float wallOfDeathVelocity = 0;
    public float playTime;
    public bool isPaused
    {
        get { return Mathf.Approximately(Time.timeScale, 0); }
    }
    
    public ObservableList<Crewman> crewmen = new ObservableList<Crewman>(new List<Crewman>());

    public override void Awake()
    {
        base.Awake();
        System.GC.Collect(); // Collect once initially
    }

    public void TogglePause()
    {
        if (Mathf.Approximately(Time.timeScale, 0))
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    private void FixedUpdate()
    {
        this.onPreTravel?.Invoke();

        if (!Mathf.Approximately(Time.timeScale, 0))
            this.playTime += Time.fixedDeltaTime / Time.timeScale;

        this.wallOfDeathVelocity = this.wallOfDeathMinVelocity + (this.wallOfDeathSpeedup.Evaluate(this.wallOfDeath / this.distanceToTravel) * (this.wallOfDeathMaxVelocity - this.wallOfDeathMinVelocity));
        this.wallOfDeath += this.wallOfDeathVelocity * Time.fixedDeltaTime;
        this.traveled += this.ship.velocity * Time.fixedDeltaTime;
        if (this.traveled >= this.distanceToTravel)
        {
            // You made it :>
            OnGameOver(true);
            return;
        }
        else if (this.wallOfDeath >= this.traveled)
        {
            // You tried, mate :<
            OnGameOver(false);
            return;
        }
        else if (this.crewmen.Count == 0)
        {
            // Well...
            OnGameOver(false);
            return;
        }
        this.onPostTravel?.Invoke();
    }

    private void OnGameOver(bool success)
    {
        (success ? this.uiGameOverSuccess : this.uiGameOverFail).SetActive(true);
        this.enabled = false;
    }
}