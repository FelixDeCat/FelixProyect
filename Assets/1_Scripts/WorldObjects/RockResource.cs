using UnityEngine;

public class RockResource : HittableObject
{
    [SerializeField] string hitParticle;
    [SerializeField] string deathParticle;

    [SerializeField] int item_to_spawn = -1;

    [SerializeField] int minQuant = 2;
    [SerializeField] int maxQuant = 4;

    private async void Start()
    {
        bool res1 = await ParticlePool.Instance.PreLoadParticleFromResource(hitParticle, 5);
        bool res2 = await ParticlePool.Instance.PreLoadParticleFromResource(deathParticle, 2);

        Debug.Log($"{hitParticle} {(res1 ? " <color=green>se pudo ": " <color=red>no se pudo ")} cargar");
        Debug.Log($"{deathParticle} {(res2 ? " <color=green>se pudo " : " <color=red>no se pudo ")} cargar");
    }

    public override void OnHit(DamageData data)
    {
         ParticlePool.Instance.Spawn(hitParticle, this.transform.position);
    }
    public override void OnDeath()
    {
        ParticlePool.Instance.Spawn(deathParticle, this.transform.position);
        ItemSpawner.SpawnItem(item_to_spawn, this.transform.position, Random.Range(minQuant,maxQuant));
        Invoke(nameof(Destry), 0.1f);
    }

    void Destry()
    {
        Destroy(this.gameObject);
    }
    


    
}
