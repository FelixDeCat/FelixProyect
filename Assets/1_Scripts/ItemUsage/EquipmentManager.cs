using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class EquipmentManager
{
    Dictionary<EquipableType, EquipSlotHandler> equip = new Dictionary<EquipableType, EquipSlotHandler>();
    [SerializeField] Transform parent;

    [SerializeField] ParticleSystem particle_equip;

    HashSet<string> guids = new HashSet<string>();

    public bool IsGUIDEquiped(string _guid)
    {
        return guids.Contains(_guid);
    }

    // Equipable Behaviour Base == Objeto instanciado en Game
    // Equip Slot Handler == Maneja los slots que estan equipados
    public UseResult TryEquip(EquipableBehaviourBase current, Slot slot)
    {
        EquipableType equipable_type = current.Type;

        if (!equip.TryGetValue(equipable_type, out EquipSlotHandler info))
        {
            Debug.LogWarning($"ItemUseManager: ranura no registrada: {equipable_type}");
            info = new EquipSlotHandler(
                _parent: parent,
                _onAddGUID: guid => guids.Add(guid),
                _onRemGUID: guid => guids.Remove(guid)
                );

            equip[equipable_type] = info;
        }

        if (info.Ocuppied())
        {
            if (info.SameItem(slot))
            {
                info.UnEquipCurrent();
                return UseResult.Success;
            }
            else
            {
                info.UnEquipCurrent();
            }
        }

        var result = info.Equip(current, slot);
        if (result == UseResult.Success && particle_equip != null)
        {
            particle_equip.Stop();
            particle_equip.transform.position = EquipDataManager.Instance.GetPosition(equipable_type);
            particle_equip.Play();
        }
        return result;
    }
}
