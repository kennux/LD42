using UnityEngine;
using UnityTK;

/// <summary>
/// UI selection
/// </summary>
public class UICrewmanSelection : SingletonBehaviour<UICrewmanSelection>
{
    [Header("Config")]
    public LayerMask raycastMask;

    /// <summary>
    /// The marker gameobject.
    /// </summary>
    public GameObject marker;

    public Crewman selectedCrewman
    {
        get { return this._selectedCrewman; }
    }

    [Header("Debug")]
    [SerializeField]
    /// <summary>
    /// The currently selected crewman
    /// </summary>
    private Crewman _selectedCrewman;

    public void Select(Crewman crewman)
    {
        this._selectedCrewman = crewman;
    }

    public void Update()
    {
        if (Essentials.UnityIsNull(this._selectedCrewman))
        {
            this._selectedCrewman = null;
            this.marker.SetActive(false);
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rh;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out rh, float.PositiveInfinity, this.raycastMask))
            {
                var crewman = rh.collider.GetComponentInParent<Crewman>();
                if (!ReferenceEquals(crewman, null))
                    Select(crewman);
                else
                {
                    Select(null);
                    return;
                }
            }
            else
            {
                Select(null);
                return;
            }
        }

        if (!ReferenceEquals(this._selectedCrewman, null))
        {
            this.marker.transform.position = this._selectedCrewman.transform.position + (Vector3.up * .01f);
            this.marker.SetActive(true);
        }
    }
}