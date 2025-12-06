using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum ActionResultOnUse
{
    nothing,
    consume,
    equip
}

public class ItemUseManager : MonoSingleton<ItemUseManager>
{
    [SerializeField] EquipmentManager equipManager;
    [SerializeField] UsableManager useManager;

    public override void SingletonAwake()
    {

    }

    public UseResult UseBehaviour(int ID)
    {
        var data = InventoryDataCenter.Get_ItemData_ByID(ID);
        if (data == null) throw new Exception("No tengo el ID " + ID);

        var usable = InventoryDataCenter.DataBase[ID].Usable;
        var equipable = InventoryDataCenter.DataBase[ID].Equipable;

#if UNITY_EDITOR
        //este bloque es para revisar que el que esta diseñando los behaviours no ponga dos behaviours con ID iguales
        for (int i = 0; i < InventoryDataCenter.DataBase.Length; i++)
        {
            if (i == ID) continue;
            if (usable != null && InventoryDataCenter.DataBase[i].Usable != null) 
                if (usable.GetUniqueBehaviourID() != -1 && usable.GetUniqueBehaviourID() == InventoryDataCenter.DataBase[i].Usable.GetUniqueBehaviourID())
                throw new System.Exception("EXCEPCION EN EDITOR: Fijate que hay dos behaviours que tienen el mismo UniqueBehaviourID");
            if (equipable != null && InventoryDataCenter.DataBase[i].Equipable != null) 
                if (equipable.GetUniqueBehaviourID() != -1 && equipable.GetUniqueBehaviourID() == InventoryDataCenter.DataBase[i].Equipable.GetUniqueBehaviourID())
                throw new System.Exception("EXCEPCION EN EDITOR: Fijate que hay dos behaviours que tienen el mismo UniqueBehaviourID");
        }
#endif

        if (usable != null)
        {
            return useManager.TryUse(usable, ID);
        }

        if (equipable != null)
        {
            Debug.Log("TryEquip");
            return equipManager.TryEquip(equipable, ID);
        }

        Debug.Log("Esta fallando aca");
        return UseResult.Fail;
    }
}

