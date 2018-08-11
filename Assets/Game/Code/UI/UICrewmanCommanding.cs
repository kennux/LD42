using UnityEngine;
using UnityEngine.AI;
using UnityTK;

public class UICrewmanCommanding : SingletonBehaviour<UICrewmanCommanding>
{
    public LayerMask interactableMask;
    public LayerMask floorMask;

    public void Update()
    {
        var selected = UICrewmanSelection.instance.selectedCrewman;
        if (Essentials.UnityIsNull(selected))
            return;
        
        // Commanding
        if (Input.GetMouseButtonDown(1))
        {
            // First, raycast!
            RaycastHit rh;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out rh, float.PositiveInfinity, this.interactableMask))
            {
                var interactable = rh.collider.GetComponentInParent<IInteractable>();
                if (!ReferenceEquals(interactable, null))
                {
                    selected.model.interact.commandInteract.TryStart(interactable);
                    return;
                }
            }

            // Floor
            if (Physics.Raycast(r, out rh, float.PositiveInfinity, this.floorMask))
            {
                var p = rh.point;
                selected.model.movement.move.TryStart(new MovementParameters(p));
            }
        }
    }
}