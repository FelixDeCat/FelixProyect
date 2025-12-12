using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class GenericObjectLoadAndPool<T> where T : Component
{
    public Dictionary<string, ObjectPool<T>> objects = new Dictionary<string, ObjectPool<T>>();
    Action<T> _onGet;
    Action<T> _onRelease;
    Action<T, string> _onCreate;
    public GenericObjectLoadAndPool()
    {
        _onGet = null;
        _onRelease = null;
        _onCreate = null;
    }
    public GenericObjectLoadAndPool(Action<T> onGet, Action<T> onRelease)
    {
        _onGet = onGet;
        _onRelease = onRelease;
        _onCreate = null;
    }
    public GenericObjectLoadAndPool(Action<T,string> onCreate, Action<T> onGet, Action<T> onRelease)
    {
        _onGet = onGet;
        _onRelease = onRelease;
        _onCreate = onCreate;
    }
    ///////////////////////////////////////
    /// F R O M   R E S O U R C E S
    ///////////////////////////////////////
    ///
    public async Task<bool> Preload(string path, string name, int defaultCapacity = 10)
    {
        if (objects.ContainsKey(name))
        {
            if (objects[name] != null)
                return false;
        }

        ResourceRequest request = Resources.LoadAsync<T>($"{path}/{name}");
        while (!request.isDone)
        {
            await Task.Yield();
        }
        if (request.asset == null)
        {
            Debug.LogWarning($"Prefab no encontrado en Resources en {path}/{name}");
            return false;
        }
        var pool = new ObjectPool<T>
        (
            createFunc: () => 
            {
                var go = GameObject.Instantiate(request.asset as T);
                _onCreate?.Invoke(go, name);
                return go;
            },
            actionOnGet: (go) =>
            {
                go.gameObject.SetActive(true);
                _onGet?.Invoke(go);
            },
            actionOnRelease: (go) =>
            {
                go.gameObject.SetActive(false);
                _onRelease?.Invoke(go);
            },
            actionOnDestroy: (go) => { GameObject.Destroy(go.gameObject); },
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: 100
        );

        objects[name] = pool;

        CustomConsole.LogPass($"Asset cargado de {path}/{name}");
        return true;
    }

    ///////////////////////////////////////
    /// P R E F A B
    ///////////////////////////////////////
    ///
    public bool Preload(T prefab, int defaultCapacity)
    {
        string name_key = prefab.name;

        if (objects.ContainsKey(name_key))
        {
            if (objects[name_key] != null)
                return false;
        }

        var pool = new ObjectPool<T>
        (
           createFunc: () => 
           {
               var go = GameObject.Instantiate(prefab);
               _onCreate?.Invoke(go, name_key);
               return go;
           },
           actionOnGet: (go) =>
           {
               go.gameObject.SetActive(true);
               _onGet?.Invoke(go);
           },
           actionOnRelease: (go) =>
           {
               _onRelease?.Invoke(go);
               go.gameObject.SetActive(false);
           },
           actionOnDestroy: (go) => { GameObject.Destroy(go.gameObject); },
           collectionCheck: true,
           defaultCapacity: defaultCapacity,
           maxSize: 100
        );

        objects[name_key] = pool;

        CustomConsole.LogPass($"Pool Cargado");
        return true;
    }

    ///////////////////////////////////////
    /// G E T  -  R E L E A S E
    ///////////////////////////////////////
    public T Get(string name)
    {
        return objects[name].Get();
    }
    public void Release(string name_key, T go)
    {
        Debug.Log("Releseado");
        if (objects.ContainsKey(name_key))
        {
            Debug.Log("Contine, voy a releasear");
            objects[name_key].Release(go);
        }
    }
}
