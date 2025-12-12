using UnityEngine;

public class RockResource : HittableObject
{
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem deathParticle;

    private void Start()
    {
        ParticlePool.Instance.PreLoad(hitParticle.name, hitParticle); 
        ParticlePool.Instance.PreLoad(deathParticle.name, deathParticle); 
    }

    public override void OnHit(DamageData data)
    {
         ParticlePool.Instance.Spawn(hitParticle.name, hitParticle, this.transform.position);
    }
    public override void OnDeath()
    {
        ParticlePool.Instance.Spawn(deathParticle.name, deathParticle, this.transform.position);
    }

    
}
