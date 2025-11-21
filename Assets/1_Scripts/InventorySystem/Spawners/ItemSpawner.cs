using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoSingleton<ItemSpawner>
{
    ResultMsg indexOutOfRangeException;
    ResultMsg minusOne;
    ResultMsg doesNotHaveModel;
    ResultMsg sucessfull;
    Dictionary<int, ObjectPool<ItemRecolectable>> pools = new Dictionary<int, ObjectPool<ItemRecolectable>>();

    const int MAX_STEPS = 1000;

    public override void SingletonAwake()
    {
        indexOutOfRangeException = new ResultMsg(false, "Index out of range Exception");
        minusOne = new ResultMsg(false, "In Ou Of Ra Ex: minus One Index");
        doesNotHaveModel = new ResultMsg(false, "Item does not have an model");
        sucessfull = new ResultMsg(true, "Sucess!!");
    }

    public static ResultMsg SpawnItem(int indexID, Vector3 position, int quantity = 1) => Instance._spawnItem(indexID, position, quantity);
    ResultMsg _spawnItem(int indexID, Vector3 position, int quant = 1)
    {
        if (indexID >= InventoryDataCenter.DataBase.Length)
            return indexOutOfRangeException;

        if (indexID == -1) return minusOne;

        var current = InventoryDataCenter.DataBase[indexID].Model;

        if (current == null)
            return doesNotHaveModel;

        if (!pools.ContainsKey(indexID))
        {
            CreatePool(indexID, current);
        }

        int steps = 0;
        while (quant > 0)
        {
            if(steps++ > MAX_STEPS)
            {
                var ex = new System.Exception($"ItemSpawner: abortando spawn porque se superó el máximo de iteraciones ({MAX_STEPS}) para _i {indexID}");
                throw ex;
            }

            var go = pools[indexID].Get();
            int prev = quant;
            quant = go.ActivateItemRecolectable(indexID, quant);
            go.transform.position = position;

            if (quant == prev)
            {
                var ex = new System.Exception("ActiveItemRecolectable no redujo la cantidad de objetos");
                _returnItem(indexID, go);
                throw ex;
            }
        }

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
            actionOnGet: item =>
            {
                item.gameObject.SetActive(true);
                item.ResetItemRecolectable();
            },
            actionOnRelease: item =>
            {
                item.ResetItemRecolectable();
                item.gameObject.SetActive(false);
            },
            actionOnDestroy: item => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 1000);

        pools.Add(key, pool);
    }


}
