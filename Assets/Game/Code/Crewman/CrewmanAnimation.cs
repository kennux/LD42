using UnityEngine;
using UnityTK;
using UnityTK.BehaviourModel;

public class CrewmanAnimation : MonoBehaviour
{
    public string manStationParamBoolName = "manStation";
    public string repairParamBoolName = "repair";
    public string moveParamBoolName = "move";

    public Animator animator;

    public Crewman crewman
    {
        get { return this._crewman.Get(this); }
    }
    private LazyLoadedComponentRef<Crewman> _crewman = new LazyLoadedComponentRef<Crewman>();

    public void Start()
    {
        this.crewman.model.movement.move.onStart += OnMoveStart;
        this.crewman.model.movement.move.onStop += OnMoveStop;

        this.crewman.model.interact.interact.onStart += OnInteractStart;
        this.crewman.model.interact.interact.onStop += OnInteractStop;
    }

    private void OnInteractStart(IInteractable interactable)
    {
        switch (interactable.type)
        {
            case InteractionType.MAN_STATION:
                {
                    this.animator.SetBool(this.repairParamBoolName, true);
                }
                break;
            case InteractionType.REPAIR:
                {
                    this.animator.SetBool(this.manStationParamBoolName, true);
                }
                break;
        }
    }

    private void OnInteractStop()
    {
        this.animator.SetBool(this.repairParamBoolName, false);
        this.animator.SetBool(this.manStationParamBoolName, false);
    }

    private void OnMoveStart(MovementParameters p)
    {
        this.animator.SetBool(this.moveParamBoolName, true);
    }

    private void OnMoveStop()
    {
        this.animator.SetBool(this.moveParamBoolName, false);
    }
}