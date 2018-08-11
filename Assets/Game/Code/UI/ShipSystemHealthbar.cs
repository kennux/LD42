using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTK;

public class ShipSystemHealthbar : MonoBehaviour
{
    [Header("Config")]
    /// <summary>
    /// The fill image to use for visualizing the health.
    /// </summary>
    public Image fillImage;

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
            FixedUpdate();
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
    }

    public void FixedUpdate()
    {
        if (Essentials.UnityIsNull(this._tracked))
            return;

        // Update health
        this.fillImage.fillAmount = this.tracked.health.health.Get() / this.tracked.health.maxHealth.Get();
    }
}
