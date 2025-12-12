using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlePool : MonoSingleton<ParticlePool>
{
    public override void SingletonAwake()
    {

    }

    readonly Dictionary<string, ObjectPool<ParticleSystem>> pools = new Dictionary<string, ObjectPool<ParticleSystem>>();

    public void PreLoad(string key, ParticleSystem prefab, int defaultCapacity = 5)
    {
        if (!pools.TryGetValue(key, out var pool))
        {
            pool = CreatePool(key, prefab);
            pools[key] = pool;
        }
    }
    public ParticleSystem Spawn(string key, ParticleSystem prefab, Vector3 pos)
    {
        if (!pools.TryGetValue(key, out var pool))
        {
            pool = CreatePool(key, prefab);
            pools[key] = pool;
        }

        var particle = pool.Get();
        particle.transform.position = pos;
        particle.Play();
        return particle;
    }

    

    public ParticleSystem SpawnAndParent(string key, ParticleSystem prefab, Transform parent)
    {
        if (!pools.TryGetValue(key, out var pool))
        {
            pool = CreatePool(key, prefab);
            pools[key] = pool;
        }

        var particle = pool.Get();
        particle.transform.SetParent(parent);
        particle.transform.position = parent.position;
        particle.Play();
        return particle;
    }

    public void Despawn(string key, ParticleSystem ps)
    {
        if (pools.TryGetValue(key, out var pool))
            pool.Release(ps);
    }


    ObjectPool<ParticleSystem> CreatePool(string key, ParticleSystem prefab)
    {
        var pool = new ObjectPool<ParticleSystem>(
                createFunc: () =>
                {
                    var obj = ParticleSystem.Instantiate(prefab);
                    obj.transform.SetParent(this.transform);
                    var listener = obj.GetComponent<ParticleStopListener>();
                    if (listener != null)
                    {
                        Debug.Log("Inicializando");
                        listener.Initialize(() => Despawn(key, obj));
                    }
                    return obj;
                },
                actionOnGet: ps =>
                {
                    ps.gameObject.SetActive(true);

                },
                actionOnRelease: ps =>
                {
                    ps.gameObject.SetActive(false);
                    ps.transform.SetParent(this.transform);
                },
                actionOnDestroy: ps => ParticleSystem.Destroy(ps.gameObject),
                defaultCapacity: 5
            );

        return pool;
    }

}
