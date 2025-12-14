using System;
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

    [SerializeField] Transform player_drop_root;

    Vector3 spawnOffsetPos { get => player_drop_root.position + player_drop_root.forward * 5 + player_drop_root.up; }

    const int MAX_STEPS = 1000;

    public override void SingletonAwake()
    {
        indexOutOfRangeException = new ResultMsg(false, "Index out of range Exception");
        minusOne = new ResultMsg(false, "In Ou Of Ra Ex: minus One Index");
        doesNotHaveModel = new ResultMsg(false, "Item does not have an model");
        sucessfull = new ResultMsg(true, "Sucess!!");
    }
    public static ResultMsg SpawnItemInFronOfPlayer(int indexID, int quantity = 1, string customGUID = "") =>
        Instance._spawnItem
        (
            indexID: indexID,
            position: Tools.Random_XZ_PosInBound(center: Instance.spawnOffsetPos, radius: 2f),
            quant: quantity,
            customGUID: customGUID
        );

    public static ResultMsg SpawnItem(int indexID, Vector3 position, int quantity = 1, bool group = true, string customGUID = "") => Instance._spawnItem(indexID, position, quantity, group, customGUID);
    ResultMsg _spawnItem(int indexID, Vector3 position, int quant = 1, bool group = true, string customGUID = "")
    {
        if (indexID == -1) return minusOne;

        var current = InventoryDataCenter.Get_Data_ByID(indexID).Model;

        if (current == null)
            return doesNotHaveModel;

        if (!pools.ContainsKey(indexID))
        {
            CreatePool(indexID, current);
        }

        if (InventoryDataCenter.Get_Data_ByID(indexID).Equipable != null)
        {
            var go = pools[indexID].Get();

            if (customGUID != "")
            {
                go.ActivateItemRecolectable(indexID, 1, customGUID);
            }
            else
            {
                go.ActivateItemRecolectable(indexID, 1, Guid.NewGuid().ToString());
            }

            go.transform.position = position;


            return sucessfull;
        }
        else
        {
            if (group)
            {
                int steps = 0;
                while (quant > 0)
                {
                    if (steps++ > MAX_STEPS)
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
            }
            else
            {
                for (int i = 0; i < quant; i++)
                {
                    var go = pools[indexID].Get();
                    go.ActivateItemRecolectable(indexID, 1);
                    go.transform.position = position;
                }
            }

            return sucessfull;
        }
    }

    public static void ReturnItem(int key, ItemRecolectable item) => Instance._returnItem(key, item);
    void _returnItem(int key, ItemRecolectable item)
    {
        if (pools.ContainsKey(key))
        {
            pools[key].Release(item);
        }
        else
        {
            CreatePool(key, item);
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
                go.name = "p" + go.name;
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
                item.transform.SetParent(this.transform);
                item.gameObject.SetActive(false);
            },
            actionOnDestroy: item => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 1000);

        pools.Add(key, pool);
    }


}
