using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK;

public class TrackGameObjet : MonoBehaviour
{
    public GameObject tracked;

    /// <summary>
    /// Rect transform cached component ref.
    /// </summary>
    public RectTransform rectTransform
    {
        get { return this._rectTransform.Get(this); }
    }
    private LazyLoadedComponentRef<RectTransform> _rectTransform = new LazyLoadedComponentRef<RectTransform>();

    public virtual void Update()
    {
        if (Essentials.UnityIsNull(this.tracked))
            return;

        // Update position
        this.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.tracked.transform.position);
    }
}
