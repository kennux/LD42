using UnityEngine;
using UnityEngine.AI;
using UnityTK;

public class UICrewmanCommanding : SingletonBehaviour<UICrewmanCommanding>
{
    public LayerMask interactableMask;
    public LayerMask floorMask;

    public void Update()
    {
        var selected = UISelection.instance.selectedCrewman;
        if (Essentials.UnityIsNull(selected))
            return;
        
        // Commanding
        if (Input.GetMouseButtonDown(1))
        {
            // First, raycast!
            RaycastHit rh;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.Log("Commanding! " + UISelection.instance.hoveringInteractable);
            if (!Essentials.UnityIsNull(UISelection.instance.hoveringInteractable))
            {
                selected.model.interact.commandInteract.TryStart(UISelection.instance.hoveringInteractable);
                return;
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