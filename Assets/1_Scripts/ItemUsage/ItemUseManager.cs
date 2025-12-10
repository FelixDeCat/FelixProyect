using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class ItemUseManager
{
    [SerializeField] EquipmentManager equipManager;
    [SerializeField] UsableManager useManager;

    public UseResult ForceDesequip(Slot slot)
    {
        var data = InventoryDataCenter.Get_Data_ByID(slot.IndexID);
        if (data.Equipable != null)
        {
            return equipManager.ForceDesequip(data.Equipable.Type, slot);
        }
        else return UseResult.Fail;

            
    }

    public async Task<UseResult> UseBehaviour(Slot slot)
    {
        var data = InventoryDataCenter.Get_Data_ByID(slot.IndexID);
        if (data == null) throw new Exception("No tengo el ID " + slot.IndexID);

        var usable = data.Usable;
        var equipable = data.Equipable;

#if UNITY_EDITOR

        //este bloque es para revisar que el que esta diseñando los behaviours no ponga dos behaviours con ID iguales
        foreach (var kvp in InventoryDataCenter.AllData) 
        {
            var currentData = kvp.Value;
            if (currentData.ItemID == slot.IndexID) continue;

            if (usable != null && currentData.Usable != null)
            {
                if (usable.GetUniqueBehaviourID() != -1 && usable.GetUniqueBehaviourID() == currentData.Usable.GetUniqueBehaviourID())
                    throw new System.Exception("EXCEPCION EN EDITOR: Fijate que hay dos behaviours que tienen el mismo UniqueBehaviourID");
            }
            if (equipable != null && currentData.Equipable != null)
            {
                if (equipable.GetUniqueBehaviourID() != -1 && equipable.GetUniqueBehaviourID() == currentData.Equipable.GetUniqueBehaviourID())
                    throw new System.Exception("EXCEPCION EN EDITOR: Fijate que hay dos behaviours que tienen el mismo UniqueBehaviourID");
            }
        }
#endif

        if (usable != null)
        {
            return useManager.TryUse(usable, slot);
        }

        if (equipable != null)
        {
            if (equipable.HasVisuals)
            {
                var res = await InstanceResourceManager.PreLoad("Equipables", data.Name);
                if (!res)
                {
                    CustomConsole.LogError("Se esta intentando cargar un Equipable que tiene visual, pero no se encuentra como Asset");
                    return UseResult.Fail;
                }
            }

            return equipManager.TryEquip(equipable, slot);
        }

        CustomConsole.Log($"Item: {data.name} no tiene equipable ni consumible");
        return UseResult.Fail;
    }

    public bool GuidEquiped(string GUID)
    {
        return equipManager.IsGUIDEquiped(GUID);
    }

    
}

