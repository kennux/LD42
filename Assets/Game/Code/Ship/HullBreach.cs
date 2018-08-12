using UnityEngine;
using UnityTK.Audio;
using UnityTK;

[RequireComponent(typeof(HealthMechanic))]
public class HullBreach : MonoBehaviour, IInteractable
{
    public AudioEvent breachSound;
    public UTKAudioSource audioSource;
    public Transform rendererTransform;
    public OxygenDrainer oxygenDrainer;


    public float draining = 12f;
    public Vector3 scaleMin;
    public Vector3 scaleMax;

    public HealthMechanic health
    {
        get { return this._health.Get(this); }
    }

    public InteractionType type
    {
        get
        {
            return InteractionType.REPAIR;
        }
    }

    public InteractionActivity interact => this._interact;
    private InteractionActivity _interact = new InteractionActivity();

    public Vector3 interactionPosition => this.transform.position;

    public Quaternion interactionLookRotation => this.transform.rotation;

    private LazyLoadedComponentRef<HealthMechanic> _health = new LazyLoadedComponentRef<HealthMechanic>();
    private Crewman currentInteractor;
    public float hps;

    public void Awake()
    {
        this.breachSound.Play(this.audioSource, true);
        this.health.takeDamage.Fire(this.health.maxHealth.Get());
        this.health.fullyHealed.handler += OnFullyHealed;

        this._interact.RegisterStartCondition(CanStartInteraction);
        this._interact.onStart += OnStartInteraction;
        this._interact.RegisterStopCondition(CanStopInteraction);
        this._interact.onStop += OnStopInteraction;
    }

    private void OnStartInteraction(Crewman crewman)
    {
        this.currentInteractor = crewman;
    }

    private void OnStopInteraction()
    {
        this.currentInteractor = null;
    }

    private bool CanStartInteraction(Crewman crewman)
    {
        return Essentials.UnityIsNull(this.currentInteractor);
    }

    private bool CanStopInteraction()
    {
        return !Essentials.UnityIsNull(this.currentInteractor);
    }

    public void Start()
    {
        this.health.takeDamage.Fire(this.health.maxHealth.Get());
    }

    private void FixedUpdate()
    {
        if (!Essentials.UnityIsNull(this.currentInteractor))
            this.health.heal.Fire(this.hps * Time.fixedDeltaTime);

        float t = this.health.health.Get() / this.health.maxHealth.Get();
        this.rendererTransform.localScale = Vector3.Lerp(this.scaleMax, this.scaleMin, t);
        this.oxygenDrainer.drainRate = Mathf.Lerp(this.draining, 0, t);
    }

    private void OnDestroy()
    {
        if (!Essentials.UnityIsNull(this.currentInteractor))
            this.currentInteractor.model.interact.interact.ForceStop();
    }

    private void OnFullyHealed()
    {
        Destroy(this.gameObject);
    }
}