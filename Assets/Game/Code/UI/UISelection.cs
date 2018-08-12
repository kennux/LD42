using UnityEngine;
using UnityTK;
using System.Collections.Generic;

/// <summary>
/// UI selection
/// </summary>
public class UISelection : SingletonBehaviour<UISelection>
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

    public ShipSystem selectedSystem
    {
        get { return this._selectedSystem; }
    }

    [Header("Debug")]
    [SerializeField]
    /// <summary>
    /// The currently selected crewman
    /// </summary>
    private Crewman _selectedCrewman;

    [SerializeField]
    /// <summary>
    /// The currently selected crewman
    /// </summary>
    private ShipSystem _selectedSystem;
    
    public IInteractable hoveringInteractable;

    private RaycastHit[] hits = new RaycastHit[16];
    [SerializeField]
    private List<Collider> hitsDebug = new List<Collider>();
    [SerializeField]
    private bool isOverUiDebug;

    public bool hasCrewmanSelected
    {
        get { return !Essentials.UnityIsNull(this._selectedCrewman); }
    }

    private List<Renderer> highlightedRenderers = new List<Renderer>();

    public void SelectSystem(ShipSystem system)
    {
        this._selectedSystem = system;
    }

    public void SelectMan(Crewman crewman)
    {
        this._selectedCrewman = crewman;
    }

    private MaterialPropertyBlock highlightBlock;
    private MaterialPropertyBlock emptyBlock;

    private void Start()
    {
        this.highlightBlock = new MaterialPropertyBlock();
        this.emptyBlock = new MaterialPropertyBlock();
        this.highlightBlock.SetColor("_Color", Color.green);
        this.emptyBlock.SetColor("_Color", Color.white);
    }

    public void Update()
    {
        // Clear prev highlights
        foreach (var hr in this.highlightedRenderers)
        {
            if (!Essentials.UnityIsNull(hr))
                hr.SetPropertyBlock(this.emptyBlock);
        }
        this.highlightedRenderers.Clear();

        if (Essentials.UnityIsNull(this._selectedCrewman))
        {
            this._selectedCrewman = null;
            this.marker.SetActive(false);
        }

        this.isOverUiDebug = false;
        this.hitsDebug.Clear();
        if (Util.IsPointerOverUI())
        {
            this.isOverUiDebug = true;
            return;
        }

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        int amt = Physics.RaycastNonAlloc(r, hits, float.PositiveInfinity, this.raycastMask, QueryTriggerInteraction.Ignore);
        Object selectedObj = null;

        float lastDist = float.PositiveInfinity;
        for (int i = 0; i < amt; i++)
        {
            var rh = hits[i];
            var p = rh.collider.ClosestPoint(r.origin);
            var dist = (r.origin - p).magnitude;

            if (lastDist < dist)
                continue;

            lastDist = dist;
            this.hitsDebug.Add(rh.collider);
            var crewman = rh.collider.GetComponentInParent<Crewman>();
            var ss = rh.collider.GetComponentInParent<ShipSystem>();
            var interactable = rh.collider.GetComponentInParent<IInteractable>();

            if (!ReferenceEquals(crewman, null))
            {
                selectedObj = crewman;
                break;
            }
            else if (!ReferenceEquals(ss, null))
            {
                selectedObj = ss;
            }
            else if (!ReferenceEquals(interactable, null))
            {
                selectedObj = interactable as Object;
            }
        }

        // Hovering
        this.hoveringInteractable = null;
        if (selectedObj is IInteractable)
            this.hoveringInteractable = selectedObj as IInteractable;

        // Highlight
        if (!ReferenceEquals(selectedObj, null))
        {
            List<Renderer> renderers = ListPool<Renderer>.Get();

            var go = (selectedObj as MonoBehaviour).gameObject;
            go.GetComponentsInChildren<Renderer>(renderers);

            foreach (var renderer in renderers)
            {
                renderer.SetPropertyBlock(this.highlightBlock);
                this.highlightedRenderers.Add(renderer);
            }

            ListPool<Renderer>.Return(renderers);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObj is Crewman)
                SelectMan(selectedObj as Crewman);
            else if (selectedObj is ShipSystem)
                SelectSystem(selectedObj as ShipSystem);
            else
            {
                SelectMan(null);
                SelectSystem(null);
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