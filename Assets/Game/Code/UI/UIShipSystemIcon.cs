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

    public Image uiImage;
    public Color imageTintOk;
    public Color imageTintDead;
    public Outline outline;
    public Color defaultOutlineColor;
    public Color mannedOutlineColor;
    public Color repairOutlineColor;
    public CanvasGroup canvasGroup;
    public float flashSpeedWhenNotFullHealthMin = 5f;
    public float flashSpeedWhenNotFullHealthMax = 15f;

    public override void Update()
    {
        base.Update();

        if (this.system.isManned && this.system.type == InteractionType.MAN_STATION)
            this.outline.effectColor = this.mannedOutlineColor;
        else if (this.system.isManned && this.system.type == InteractionType.REPAIR)
            this.outline.effectColor = this.repairOutlineColor;
        else
            this.outline.effectColor = this.defaultOutlineColor;

        float h = this.system.health.health.Get() / this.system.health.maxHealth.Get();
        this.uiImage.color = Color.Lerp(this.imageTintDead, this.imageTintOk, h);

        if (!this.system.fullHealth && !this.system.isManned)
        {
            float s = Mathf.Lerp(this.flashSpeedWhenNotFullHealthMin, this.flashSpeedWhenNotFullHealthMax, h);
            this.canvasGroup.alpha = Mathf.Sin(Time.time * s) > 0 ? 1 : 0;
        }
        else
        {
            this.canvasGroup.alpha = 1;
        }
    }
}
