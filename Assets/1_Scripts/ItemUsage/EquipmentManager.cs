using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class EquipmentManager
{
    Dictionary<EquipableType, EquipedItemInfo> equip = new Dictionary<EquipableType, EquipedItemInfo>();
    [SerializeField] Transform parent;


    public UseResult TryEquip(EquipableBehaviour current, int ID)
    {
        EquipableType equipable_type = current.Type;

        if (!equip.TryGetValue(equipable_type, out EquipedItemInfo info))
        {
            Debug.LogWarning($"ItemUseManager: ranura no registrada: {equipable_type}");
            info = new EquipedItemInfo(_parent: parent);
            equip[equipable_type] = info;
        }

        if (info.Ocuppied())
        {
            if (info.SameID(ID))
            {
                info.UnEquipCurrent();
                return UseResult.Success;
            }
            else
            {
                info.UnEquipCurrent();
            }
            
        }

        return info.Equip(ID, current);
    }
}
