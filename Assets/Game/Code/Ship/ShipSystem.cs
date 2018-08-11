using UnityEngine;
using UnityTK;

/// <summary>
/// Abstract base class for implementing ship systems.
/// </summary>
[RequireComponent(typeof(ShipSystemModel), typeof(HealthMechanic))]
public abstract class ShipSystem : MonoBehaviour, IInteractable
{
    public ShipSystemModel model
    {
        get { return this._model.Get(this); }
    }
    private LazyLoadedComponentRef<ShipSystemModel> _model = new LazyLoadedComponentRef<ShipSystemModel>();

    /// <summary>
    /// The position from which to interact.
    /// </summary>
    public Transform interactionAnchor;

    public InteractionType type
    {
        get
        {
            return InteractionType.MAN_STATION;
        }
    }

    public InteractionActivity interact
    {
        get
        {
            return _interact;
        }
    }

    public Vector3 interactionPosition
    {
        get
        {
            return interactionAnchor.position;
        }
    }

    public Quaternion interactionLookRotation
    {
        get
        {
            return interactionAnchor.rotation;
        }
    }

    protected InteractionActivity _interact = new InteractionActivity();

    public HealthMechanic health
    {
        get { return this._health.Get(this); }
    }
    private LazyLoadedComponentRef<HealthMechanic> _health = new LazyLoadedComponentRef<HealthMechanic>();

    [Header("Config")]
    public float energyDrain = 0;
    public float efficiencyDestroyed = 0;
    public float efficiencyNormal = 1;

    /// <summary>
    /// Linearly increasing from 0 to this based on crewman skill for this system.
    /// </summary>
    public float efficiencyMannedAdd = 1;

    /// <summary>
    /// How many health is restored per second when this is being repaired?
    /// </summary>
    public float repairHealRate = 1f;

    /// <summary>
    /// Computes current efficiency.
    /// </summary>
    public float currentEfficiency
    {
        get
        {
            var health01 = this.health.health.Get() / this.health.maxHealth.Get();
            float eff = Mathf.Lerp(this.efficiencyDestroyed, this.efficiencyNormal, health01);

            if (this.fullHealth && !Essentials.UnityIsNull(this.currentInteractor))
                eff += this.currentInteractor.model.exp.getExperienceMultiplicator.Invoke(this.shipSystemType) * this.efficiencyMannedAdd;

            return eff;
        }
    }

    public bool fullHealth
    {
        get { return Mathf.Approximately(this.health.health.Get(), this.health.maxHealth.Get()); }
    }
    
    public float theoreticalMaxEfficiency { get { return this.efficiencyNormal + this.efficiencyMannedAdd; } }
    public float lastEfficiency { get { return this._lastEfficiency; } }
    [SerializeField]
    private float _lastEfficiency;

    /// <summary>
    /// How much energy drain could be statisfied this frame, <see cref="UpdateEnergyDrain"/>
    /// </summary>
    private float currentFrameEnergyDrainModifier;

    public void Awake()
    {
        this._interact.RegisterStartCondition(this.CanStartInteraction);
        this._interact.RegisterStopCondition(this.CanStopInteraction);
        this._interact.onStart += OnStartInteraction;
        this._interact.onStop += OnStopInteraction;

        this.health.takeDamage.handler += OnTakeDamageInteraction;
        this.health.fullyHealed.handler += OnFullyHealed;
    }

    public void Start()
    {
        Ship.instance.systems.Add(this);
    }

    public void Update()
    {
        UpdateInteraction();

        UpdateEnergyDrain();
        this._lastEfficiency = this.currentEfficiency;
        UpdateSystem(this._lastEfficiency);

        if (!this.fullHealth && !Essentials.UnityIsNull(this.currentInteractor))
        {
            this.health.heal.Fire(this.repairHealRate * Time.deltaTime);
        }
    }

    public void OnDestroy()
    {
        Ship.instance.systems.Remove(this);
    }

    private void UpdateEnergyDrain()
    {
        // Compute workload
        this.currentFrameEnergyDrainModifier = 1;
        float workload = this.ComputeWorkLoad(this.currentEfficiency);

        float drainTarget = this.energyDrain * Time.deltaTime * workload;
        float drain = Mathf.Min(Ship.instance.energy, drainTarget);
        Ship.instance.energy.value -= drain;
        this.currentFrameEnergyDrainModifier = (drain / drainTarget);
    }

    #region Interaction

    private Crewman retriggerInteract;
    private Crewman currentInteractor;

    private void OnTakeDamageInteraction(float damage)
    {
        RetriggerInteract();
    }

    private void OnFullyHealed()
    {
        RetriggerInteract();
    }

    private void RetriggerInteract()
    {
        if (Essentials.UnityIsNull(this.currentInteractor))
            return;

        this.retriggerInteract = this.currentInteractor;
        this.currentInteractor.model.interact.interact.ForceStop();
    }

    private bool CanStartInteraction(Crewman crewman)
    {
        return Essentials.UnityIsNull(this.currentInteractor);
    }

    private bool CanStopInteraction()
    {
        return !Essentials.UnityIsNull(this.currentInteractor);
    }

    private void OnStartInteraction(Crewman crewman)
    {
        this.currentInteractor = crewman;
    }

    private void OnStopInteraction()
    {
        if (Essentials.UnityIsNull(this.currentInteractor))
            return;

        this.currentInteractor = null;
    }

    private void UpdateInteraction()
    {
        if (!Essentials.UnityIsNull(this.retriggerInteract))
        {
            this.retriggerInteract.model.interact.interact.TryStart(this);
            this.retriggerInteract = null;
        }
    }

    #endregion

    #region Abstract

    /// <summary>
    /// The type of this system, used for gaining experience.
    /// </summary>
    public abstract ShipSystemType shipSystemType { get; }

    /// <summary>
    /// Called from <see cref="Update"/> to update system.
    /// </summary>
    /// <param name="currentEfficiency"><see cref="currentEfficiency"/> evaluation result.</param>
    protected abstract void UpdateSystem(float currentEfficiency);

    /// <summary>
    /// Computes the workload this system would have in the current frame.
    /// This will determine the energy drain amount in <see cref="UpdateEnergyDrain"/>
    /// </summary>
    /// <param name="predictedEfficiency">Predicted efficiency of this system update in current frame.</param>
    /// <returns>0-1 amount of work load</returns>
    protected abstract float ComputeWorkLoad(float predictedEfficiency);

    #endregion

}