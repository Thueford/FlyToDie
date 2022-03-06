using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [NotNull] public SphereCollider sc;
    [NotNull] public Particles ps;
    private float lifespan = 0.1f, damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StopCollision", lifespan);
        SoundHandler.PlayClip("explosion");
    }

    // destroyed by ParticleSystem
    // void StopExplosion() { Destroy(gameObject); }
    void StopCollision() => sc.enabled = false;


    public void SetProperties(string owner, float radius, float damage, float velScale = 3)
    {
        gameObject.layer = owner == "Player" ? 16 : 15;
        tag = owner + "Bullet";
        sc.radius = radius;

        ps.vel.scale = velScale * sc.radius * ps.vel.scale.normalized;
        ps.properties.lifetime = sc.radius / ps.vel.scale.x;
        ps.size.val.z = -ps.size.val.y / ps.properties.lifetime;

        // light.range = p.explosionRadius*2;
        this.damage = damage;
        // if (damage >= 10) SoundHandler.SetLPTarget(1500, 2);
    }

    private void hit(Vector3 pos)
    {
        // Instantiate(hitPrefab, pos, Quaternion.identity);
    }
}
