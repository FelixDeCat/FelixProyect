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
    public static Item Get_Item_ByID(int ID)
    {
        if (ID == -1)
        {
            return Item.ItemNull;
        }
        if (ID >= Instance.items.Length || ID < 0) 
        {
            CustomConsole.LogError($"Intentando obtener Item con ID fuera de Rango -> <color=yellow>ID: {ID}</color>");
            return null;
        }
        return Instance.items[ID];
    }
    public static ItemData Get_ItemData_ByID(int ID)
    {
        if (ID >= Instance.items.Length || ID < 0) 
        {
            CustomConsole.LogError($"Intentando obtener ItemData con ID fuera de Rango -> <color=yellow>ID: {ID}</color>");
            return null;
        }
        return Instance.dataBase[ID];
    }
    public static ItemData[] DataBase
    {
        get
        {
            return Instance.dataBase;
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





