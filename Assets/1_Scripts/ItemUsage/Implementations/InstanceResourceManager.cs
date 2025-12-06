using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class InstanceResourceManager : MonoSingleton<InstanceResourceManager>
{
    public Dictionary<string,ObjectPool<GameObject>> objects = new Dictionary<string, ObjectPool<GameObject>>();

    public override void SingletonAwake()
    {
        
    }

    public static async Task<GameObject> FindAndPoolObject(string path, string name)
    {
        return await Instance._findAndPoolObject(path, name);
    }

    public static void Release(string name, GameObject go)
    {
        Instance._release(name, go);
    }
    void _release(string name, GameObject go)
    {
        if (objects.ContainsKey(name))
        {
            objects[name].Release(go);
        }
    }

    public async Task<GameObject> _findAndPoolObject(string path, string name)
    {
        if (objects.TryGetValue(name, out ObjectPool<GameObject> existingPool))
        {
            return existingPool.Get();
        }

        ResourceRequest request = Resources.LoadAsync<GameObject>($"{path}/{name}");
        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.asset == null)
        {
            Debug.LogWarning($"Prefab no encontrado en Resources en {path}/{name}");
            return null;
        }

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>
            (
            createFunc: () => Instantiate(request.asset as GameObject),
            actionOnGet: (go) => { go.SetActive(true); },
            actionOnRelease: (go) => { go.SetActive(false); },
            actionOnDestroy: (go) => { Destroy(go); },
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
        );

        objects[name] = pool;

        return pool.Get();

    }
}
