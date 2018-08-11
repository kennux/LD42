using UnityEngine;
using UnityTK;

/// <summary>
/// UI selection
/// </summary>
public class UIShipSystemSelection : SingletonBehaviour<UIShipSystemSelection>
{
    [Header("Config")]
    public LayerMask raycastMask;

    public ShipSystem selectedSystem
    {
        get { return this._selectedSystem; }
    }

    [Header("Debug")]
    [SerializeField]
    /// <summary>
    /// The currently selected crewman
    /// </summary>
    private ShipSystem _selectedSystem;

    public void Select(ShipSystem system)
    {
        this._selectedSystem = system;
    }

    public void Update()
    {
        if (Essentials.UnityIsNull(this._selectedSystem))
        {
            this._selectedSystem = null;
        }

        if (Util.IsPointerOverUI())
            return;

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rh;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out rh, float.PositiveInfinity, this.raycastMask, QueryTriggerInteraction.Ignore))
            {
                var ss = rh.collider.GetComponentInParent<ShipSystem>();
                if (!ReferenceEquals(ss, null))
                    Select(ss);
            }
        }
    }
}