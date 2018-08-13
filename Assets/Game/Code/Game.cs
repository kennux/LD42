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

    [System.Serializable]
    public struct EdgeOfSpaceDifficultySetting
    {
        public GameDifficulty difficulty;
        public AnimationCurve speedup;

        public float maxVelocity;
        public float minVelocity;
        public float distanceMaxVelocity;
    }

    [Header("Game Settings")]
    public EdgeOfSpaceDifficultySetting[] difficulties;
    public float distanceToTravel = 1000000;
    public Ship ship;

    public GameObject uiGameOverFail;
    public GameObject uiGameOverSuccess;
    public GameDifficulty nullDifficulty = GameDifficulty.MEDIUM;

    [Header("Debug")]
    public float traveled = 0;
    public float wallOfDeath = 0;
    public float wallOfDeathVelocity = 0;
    public float playTime;
    public bool isPaused
    {
        get { return Mathf.Approximately(Time.timeScale, 0); }
    }
    private float _timeScale = 1;

    [Header("Debug (EoS)")]
    [SerializeField]
    private AnimationCurve edgeOfSpaceSpeedup;
    [SerializeField]
    private float edgeOfSpaceMaxVelocity;
    [SerializeField]
    private float edgeOfSpaceMinVelocity;
    [SerializeField]
    private float edgeOfSpaceDistanceMaxVelocity;

    public ObservableList<Crewman> crewmen = new ObservableList<Crewman>(new List<Crewman>());

    public override void Awake()
    {
        base.Awake();

        var difficulty = MainMenu.difficulty;
        if (difficulty == GameDifficulty.NULL)
            difficulty = nullDifficulty;

        foreach (var diff in this.difficulties)
        {
            if (diff.difficulty == difficulty)
            {
                // Apply
                this.edgeOfSpaceSpeedup = diff.speedup;
                this.edgeOfSpaceMinVelocity = diff.minVelocity;
                this.edgeOfSpaceMaxVelocity = diff.maxVelocity;
                this.edgeOfSpaceDistanceMaxVelocity = diff.distanceMaxVelocity;
            }
        }

        System.GC.Collect(); // Collect once initially
    }

    public void TogglePause()
    {
        if (Mathf.Approximately(Time.timeScale, 0))
            Time.timeScale = this._timeScale;
        else
            Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this._timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this._timeScale = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this._timeScale = 5;
        }

        if (!this.isPaused)
            Time.timeScale = this._timeScale;
    }

    private void FixedUpdate()
    {
        this.onPreTravel?.Invoke();

        if (!Mathf.Approximately(Time.timeScale, 0))
            this.playTime += Time.fixedDeltaTime / Time.timeScale;

        this.wallOfDeathVelocity = this.edgeOfSpaceMinVelocity + (this.edgeOfSpaceSpeedup.Evaluate(this.wallOfDeath / this.distanceToTravel) * (this.edgeOfSpaceMaxVelocity - this.edgeOfSpaceMinVelocity));
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
        Ship.instance.enabled = false;
    }
}