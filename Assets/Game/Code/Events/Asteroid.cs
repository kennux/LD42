using UnityEngine;
using UnityTK.Audio;
using System.Collections.Generic;
using UnityTK;

public class Asteroid : MonoBehaviour
{
    public UTKAudioSource audioSource;
    public AudioEvent collisionAudio;
    public GameObject debris;
    public float destroyDebrisAfter = 2f;

    public AnimationCurve damageFalloff;
    public float aoeRadius;
    public float damage;
    public float speed = 25f;
    public Vector3 target;
    public Vector3 movementDir;
    public LayerMask aoeLayerMask;

    public float hullBreachChance = .2f;
    private float moveTime;

    public void Initialize(GameObject target, float startMovingAfter)
    {
        this.target = target.transform.position;
        this.movementDir = (this.target - this.transform.position).normalized;
        this.moveTime = Time.time + startMovingAfter;
    }

    public void FixedUpdate()
    {
        if (this.moveTime > Time.time)
            return;

        var delta = (this.target - this.transform.position).normalized;
        this.transform.position += this.movementDir * this.speed * Time.fixedDeltaTime;

        if (Vector3.Dot(this.movementDir, delta) < 0.5f)
        {
            // We did hit!
            this.Hit();
        }
    }

    private void Hit()
    {
        this.transform.position = this.target;

        // Visuals
        var debrisGo = Instantiate(this.debris, this.transform.position, this.transform.rotation);
        Destroy(debrisGo, this.destroyDebrisAfter);
        this.collisionAudio.Play(this.audioSource);
        Destroy(this.gameObject, this.audioSource.clip.length * this.audioSource.pitch);

        // Disable renderers
        List<Renderer> renderers = ListPool<Renderer>.Get();
        GetComponentsInChildren<Renderer>(renderers);
        foreach (var renderer in renderers)
            renderer.enabled = false;
        ListPool<Renderer>.Return(renderers);

        // Disable logic
        this.enabled = false;

        // Deal damage
        foreach (var hit in Physics.OverlapSphere(this.transform.position, this.aoeRadius, this.aoeLayerMask))
        {
            var health = hit.GetComponentInParent<HealthMechanic>();
            if (ReferenceEquals(health, null))
                continue;

            float dist = (hit.ClosestPoint(this.transform.position) - this.transform.position).magnitude;
            float dmg = this.damageFalloff.Evaluate(dist / this.aoeRadius) * this.damage * (1f - Ship.instance.damageMitigation);
            health.takeDamage.Fire(dmg);
        }

        // Hull breaching
        if (Random.value < this.hullBreachChance)
        {
            Ship.instance.SpawnHullBreach(this.transform.position);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.aoeRadius);
    }
}