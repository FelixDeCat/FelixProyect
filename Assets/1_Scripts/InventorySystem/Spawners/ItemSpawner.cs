using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoSingleton<ItemSpawner>
{
    ResultMsg indexOutOfRangeException;
    ResultMsg doesNotHaveModel;
    ResultMsg sucessfull;
    Dictionary<int, ObjectPool<ItemRecolectable>> pools = new Dictionary<int, ObjectPool<ItemRecolectable>>();

    public override void SingletonAwake()
    {
        indexOutOfRangeException = new ResultMsg(false, "Index out of range Exception");
        doesNotHaveModel = new ResultMsg(false, "Item does not have an model");
        sucessfull = new ResultMsg(true, "Sucess!!");
    }

    public static ResultMsg SpawnItem(int index, Vector3 position) => Instance._spawnItem(index, position);
    ResultMsg _spawnItem(int index, Vector3 position)
    {
        if (index >= InventoryDataCenter.DataBase.Length) 
            return indexOutOfRangeException;

        var current = InventoryDataCenter.DataBase[index].Model;

        if (current == null)
            return doesNotHaveModel;

        if (!pools.ContainsKey(index))
        {
            CreatePool(index, current);
        }

        var go = pools[index].Get();
        go.transform.position = position;

        return sucessfull;
    }

    public static void ReturnItem(int key, ItemRecolectable item) => Instance._returnItem(key, item);
    void _returnItem(int key, ItemRecolectable item)
    {
        if (pools.ContainsKey(key))
        {
            pools[key].Release(item);
        }
    }

    void CreatePool(int key, ItemRecolectable model)
    {
        if (pools.ContainsKey(key)) return;

        var pool = new ObjectPool<ItemRecolectable>(
            createFunc: () =>
            {
                var go = GameObject.Instantiate(model);
                go.transform.SetParent(this.transform);
                return go;
            },
            actionOnGet: item => item.gameObject.SetActive(true),
            actionOnRelease: item => item.gameObject.SetActive(false),
            actionOnDestroy: item => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 1000);

        pools.Add(key, pool);
    }


}
