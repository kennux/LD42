using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityTK;

public class ShipSystemPlate : MonoBehaviour
{
    [Header("Config")]
    /// <summary>
    /// The fill image to use for visualizing the health.
    /// </summary>
    [FormerlySerializedAs("fillImage")]
    public Image healthFillImage;
    public Image efficiencyFillImage;
    public Slider userWorkloadSlider;
    public GameObject userWorkloadSliderToggle;
    public CanvasGroup canvasGroup;

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
            if (!ReferenceEquals(this._tracked, value))
                this.userWorkloadSlider.value = value.userLoad;

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

    public bool hasLoadSlider { get { return !Mathf.Approximately(this.tracked.energyDrain, 0); } }

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

        bool isEnabled = ReferenceEquals(UIShipSystemSelection.instance.selectedSystem, this.tracked);
        this.canvasGroup.alpha = isEnabled ? 1 : 0;
        this.canvasGroup.blocksRaycasts = isEnabled;
        this.canvasGroup.interactable = isEnabled;
        this.tracked.userLoad = this.userWorkloadSlider.value;
        this.userWorkloadSliderToggle.SetActive(this.hasLoadSlider);
    }

    public void Close()
    {
        UIShipSystemSelection.instance.Select(null);
    }
}
