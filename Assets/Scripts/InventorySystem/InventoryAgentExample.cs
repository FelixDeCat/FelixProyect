using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAgentExample : MonoBehaviour
{
    // lo agregamos desde afuera
    [SerializeField] ItemData[] dataBase;
    public static Item[] DB;

    // ejemplo de inventario
    public Container bag;
    [SerializeField] UIContainer uiContainer;

    void Start()
    {
        DB = new Item[dataBase.Length];
        for (int i = 0; i < dataBase.Length; i++)
        {
            DB[i] = dataBase[i].data;
        }

        bag = new Container(32);

        uiContainer.Intialize(bag);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bag.Add(Random.Range(0, DB.Length), Random.Range(1,4), 0);
            uiContainer.Refresh(bag);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}





