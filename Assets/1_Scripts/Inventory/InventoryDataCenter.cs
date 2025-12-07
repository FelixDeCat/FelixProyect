using System.Collections.Generic;
using UnityEngine;

public class InventoryDataCenter : MonoSingleton<InventoryDataCenter>
{
    [SerializeField] ItemData[] _dataBase;

    Dictionary<int, ItemData> dataBase = new Dictionary<int, ItemData>();
    Dictionary<int, Item> items = new Dictionary<int, Item>();
    public override void SingletonAwake()
    {
        for (int i = 0; i < _dataBase.Length; i++)
        {
            items.Add(_dataBase[i].ItemID, new Item(
                _dataBase[i].Name,
                _dataBase[i].Description,
                _dataBase[i].MaxStack,
                _dataBase[i].Image));

            dataBase.Add(_dataBase[i].ItemID, _dataBase[i]);
        }
    }
    public static Item Get_Item_ByID(int ID, bool debug = true)
    {
        if (!Instance.items.ContainsKey(ID)) 
        {
            if(debug) CustomConsole.LogError($"No tengo -> <color=yellow>ID: {ID}</color>");
            return null;
        }
        return Instance.items[ID];
    }
    public static ItemData Get_Data_ByID(int ID)
    {
        if (!Instance.dataBase.ContainsKey(ID)) 
        {
            CustomConsole.LogError($"No tengo  -> <color=yellow>ID: {ID}</color>");
            return null;
        }
        return Instance.dataBase[ID];
    }
    public static Dictionary<int, ItemData> AllData
    {
        get
        {
            return Instance.dataBase;
        }
    }

    public static int Length
    {
        get
        {
            return Instance._dataBase.Length;
        }
    }

    public static ItemData RandomItem
    {
        get
        {
            return Instance._dataBase[Random.Range(0, Instance._dataBase.Length)];
        }
    }



}





