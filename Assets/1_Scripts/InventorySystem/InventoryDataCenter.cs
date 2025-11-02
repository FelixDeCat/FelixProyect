using System.Collections.Generic;
using UnityEngine;

public class InventoryDataCenter : MonoSingleton<InventoryDataCenter>
{
    [SerializeField] ItemData[] dataBase;
    Item[] items;
    public static Item[] DB
    {
        get
        {
            return Instance.items;
        }
    }

    public override void SingletonAwake()
    {
        items = new Item[dataBase.Length];
        for (int i = 0; i < dataBase.Length; i++)
        {
            items[i] = new Item(
                dataBase[i].Name, 
                dataBase[i].Description, 
                dataBase[i].MaxStack,
                dataBase[i].Image);
        }
    }

}





