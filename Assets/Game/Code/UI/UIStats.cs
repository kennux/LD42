using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStats : MonoBehaviour
{
    public Text velocityChangeTxt;
    public Text energyChangeTxt;
    public Text oxygenChangeTxt;
    public Text mitigationText;

    private float velocity;
    private float energy;
    private float oxygen;

    private float lastDeltaVelocity;
    private float lastDeltaEnergy;
    private float lastDeltaOxygen;

    public Color positiveColor;
    public Color negativeColor;

    private float t;

    private void Start()
    {
        this.velocity = Ship.instance.velocity;
        this.energy = Ship.instance.energy;
        this.oxygen = Ship.instance.oxygen;
        this.t = 0;
    }

    private void Update()
    {
        if (t > 1)
        {
            float v = Ship.instance.velocity;
            float e = Ship.instance.energy;
            float o = Ship.instance.oxygen;
            float vDelta = float.Parse((v - this.velocity).ToString("0.00")), eDelta = float.Parse((e - this.energy).ToString("0.00")), oDelta = float.Parse((o - this.oxygen).ToString("0.00"));

            this.velocityChangeTxt.text = vDelta.ToString("0.00");
            this.energyChangeTxt.text = eDelta.ToString("0.00");
            this.oxygenChangeTxt.text = oDelta.ToString("0.00");
            this.mitigationText.text = (Ship.instance.damageMitigation * 100f).ToString("0.00") + " %";

            this.velocityChangeTxt.color = (vDelta < 0) ? this.negativeColor : this.positiveColor;
            this.energyChangeTxt.color = (eDelta < 0) ? this.negativeColor : this.positiveColor;
            this.oxygenChangeTxt.color = (oDelta < 0) ? this.negativeColor : this.positiveColor;

            this.lastDeltaEnergy = eDelta;
            this.lastDeltaOxygen = oDelta;
            this.lastDeltaVelocity = vDelta;

            Start();
        }
        else
            t += Time.deltaTime;
    }
}
