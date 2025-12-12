using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlePool : MonoSingleton<ParticlePool>
{
    const string PARTICLES_PATH = "Particles";

    GenericObjectLoadAndPool<ParticleSystem> loader;
    public override void SingletonAwake()
    {
        loader = new GenericObjectLoadAndPool<ParticleSystem>
        (
            onCreate: (obj, original_name) =>
            {
                obj.transform.SetParent(this.transform);
                var listener = obj.GetComponent<ParticleStopListener>();
                if (listener != null)
                {
                    Debug.Log("Inicializando");
                    listener.Initialize(() => Despawn(original_name,obj));
                }
            },
            onGet: obj =>
            {
                //nothing
            },
            onRelease: obj =>
            {
                Debug.Log("Releseado");
                obj.transform.SetParent(this.transform);
            }
        );
    }

    public bool PreLoadParticle(ParticleSystem prefab, int defaultCapacity = 5)
    {
        return loader.Preload(prefab, defaultCapacity);
    }
    public async Task<bool> PreLoadParticleFromResource(string particle_name_in_resource, int defaultCapacity = 5)
    {
        return await loader.Preload(PARTICLES_PATH, particle_name_in_resource, defaultCapacity);
    }
    public ParticleSystem Spawn(string key_name, Vector3 pos)
    {
        var particle = loader.Get(key_name);
        particle.transform.position = pos;
        particle.Play();
        return particle;
    }

    public ParticleSystem Spawn(string key_name, Transform parent)
    {
        var particle = loader.Get(key_name);
        particle.transform.SetParent(parent);
        particle.transform.position = parent.position;
        particle.Play();
        return particle;
    }

    public void Despawn(string name_key, ParticleSystem ps)
    {
        loader.Release(name_key, ps);
    }
}
