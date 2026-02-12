using UnityEngine;

public class HittableResource : HittableObject
{
    [SerializeField] string hitParticle;
    [SerializeField] string deathParticle;

    [SerializeField] int item_to_spawn = -1;

    [SerializeField] int minQuant = 2;
    [SerializeField] int maxQuant = 4;

    [SerializeField] ShakeComponent shakeComponent;

    private async void Start()
    {
        bool res1 = await ParticlePool.Instance.PreLoadParticleFromResource(hitParticle, 5);
        bool res2 = await ParticlePool.Instance.PreLoadParticleFromResource(deathParticle, 2);

        shakeComponent.Initialize();

        //Debug.Log($"{hitParticle} {(res1 ? " <color=green>se pudo " : " <color=red>no se pudo ")} cargar");
        //Debug.Log($"{deathParticle} {(res2 ? " <color=green>se pudo " : " <color=red>no se pudo ")} cargar");
    }

    private void Update()
    {
        if(shakeComponent != null) shakeComponent.Tick();
    }

    public override void OnHit(DamageData data)
    {
        shakeComponent.Shake();
        ParticlePool.Instance.Spawn(hitParticle, this.transform.position);
    }
    public override void OnDeath()
    {
        ParticlePool.Instance.Spawn(deathParticle, this.transform.position);
        ItemSpawner.SpawnItem
        (
            indexID:    item_to_spawn, 
            position:   transform.position + Vector3.up,
            quantity:   Random.Range(minQuant, maxQuant), 
            group:      false
        );
        Invoke(nameof(Destry), 0.1f);
    }

    void Destry()
    {
        Destroy(this.gameObject);
    }




}
