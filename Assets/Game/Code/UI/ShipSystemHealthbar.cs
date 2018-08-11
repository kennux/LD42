using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityTK;

public class ShipSystemHealthbar : MonoBehaviour
{
    [Header("Config")]
    /// <summary>
    /// The fill image to use for visualizing the health.
    /// </summary>
    [FormerlySerializedAs("fillImage")]
    public Image healthFillImage;
    public Image efficiencyFillImage;

    /// <summary>
    /// The ship system bound to this health bar.
    /// </summary>
    public ShipSystem tracked
    {
        get
        {
            return this._tracked;
        }
        set
        {
            this._tracked = value;
            Update();
        }
    }
    [Header("Debug")]
    [SerializeField]
    private ShipSystem _tracked;

    /// <summary>
    /// Rect transform cached component ref.
    /// </summary>
    public RectTransform rectTransform
    {
        get { return this._rectTransform.Get(this); }
    }
    private LazyLoadedComponentRef<RectTransform> _rectTransform = new LazyLoadedComponentRef<RectTransform>();

    public void Update()
    {
        if (Essentials.UnityIsNull(this._tracked))
            return;

        // Update position
        this.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.tracked.transform.position);

        // Update health
        this.healthFillImage.fillAmount = this.tracked.health.health.Get() / this.tracked.health.maxHealth.Get();

        // Update effciency
        this.efficiencyFillImage.fillAmount = this.tracked.lastEfficiency / this.tracked.theoreticalMaxEfficiency;
    }
}
