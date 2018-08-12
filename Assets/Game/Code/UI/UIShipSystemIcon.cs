using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTK;

public class UIShipSystemIcon : TrackGameObjet
{
    public ShipSystem system
    {
        get { return this._system.Get(this.tracked.transform); }
    }
    private LazyLoadedComponentRef<ShipSystem> _system = new LazyLoadedComponentRef<ShipSystem>();

    public Outline outline;
    public Color defaultOutlineColor;
    public Color mannedOutlineColor;
    public Color repairOutlineColor;
    public CanvasGroup canvasGroup;
    public float flashSpeedWhenNotFullHealth = 5f;

    public override void Update()
    {
        base.Update();

        if (this.system.isManned && this.system.type == InteractionType.MAN_STATION)
            this.outline.effectColor = this.mannedOutlineColor;
        else if (this.system.isManned && this.system.type == InteractionType.REPAIR)
            this.outline.effectColor = this.repairOutlineColor;
        else
            this.outline.effectColor = this.defaultOutlineColor;

        if (!this.system.fullHealth && !this.system.isManned)
        {
            this.canvasGroup.alpha = Mathf.Sin(Time.time * this.flashSpeedWhenNotFullHealth) > 0 ? 1 : 0;
        }
        else
        {
            this.canvasGroup.alpha = 1;
        }
    }
}
