using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTK;

public class ShieldAnimation : SingletonBehaviour<ShieldAnimation>
{
    public Color invisibleColor;
    public Color mitigateColor;
    public float animationSpeed = 1f;

    [SerializeField]
    private float t;

    public Material mat;
    public new Renderer renderer;

    private void Start()
    {
    }

    public void Mitigate()
    {
        if (Ship.instance.damageMitigation > 0)
            this.t = 1;
    }

    public void FixedUpdate()
    {
        this.mat.SetColor("_Color", Color.Lerp(this.invisibleColor, this.mitigateColor, this.t));
        this.t = Mathf.Lerp(this.t, 0, this.animationSpeed * Time.fixedDeltaTime);
    }
}
