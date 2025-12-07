using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotHandler
{

    Transform parent;
    EquipableBehaviourBase current_behaviour;

    public EquipSlotHandler(Transform _parent)
    {
        current_behaviour = null;
        this.parent = _parent;
    }

    public UseResult Equip(EquipableBehaviourBase instance_model, Slot slot)
    {
        if (instance_model == null) return UseResult.Fail;

        EquipSlot newSlot = slot.GetEquipSlot();

        if (current_behaviour == null)
        {
            LogicEquip(instance_model, newSlot);
            return UseResult.Success;
        }
        else
        {
            EquipSlot lastSlot = current_behaviour.GetSlotData();

            // Si ya es la misma instancia y mismo ID, no hacer nada (toggle lo gestiona el manager)
            if (lastSlot.index_id == newSlot.index_id &&
                lastSlot.parameters == newSlot.parameters)
            {
                CustomConsole.Log("Mismo Item, no hago nada");
                return UseResult.Success;
            }

            // desequipo
            if (current_behaviour != instance_model)
            {

                current_behaviour.UnEquip();
                if (current_behaviour.gameObject != null)
                {
                    CustomConsole.Log("Desequipando... " + current_behaviour.GetSlotData().index_id);
                    GameObject.Destroy(current_behaviour.gameObject);
                    current_behaviour = null;
                }
            }

            LogicEquip(instance_model, newSlot);

            return UseResult.Success;
        }
    }

    void LogicEquip(EquipableBehaviourBase _behaviour, EquipSlot slot)
    {
        EquipableBehaviourBase newCurrent = GameObject.Instantiate(_behaviour, parent);
        current_behaviour = newCurrent;
        current_behaviour.gameObject.SetActive(true);
        current_behaviour.Equip(slot);
    }

    public UseResult UnEquipCurrent()
    {
        if (current_behaviour == null || current_behaviour.GetSlotData().index_id == -1)
            return UseResult.Fail;

        current_behaviour.UnEquip();

        if (current_behaviour.gameObject != null)
            GameObject.Destroy(current_behaviour.gameObject);

        current_behaviour = null;

        return UseResult.Success;
    }

    public bool SameItem(Slot slot)
    {
        var a = current_behaviour.GetSlotData();
        var b = slot.GetEquipSlot();

        return a.index_id == b.index_id && a.parameters == b.parameters && a.GUID == b.GUID;
    }
    public bool Ocuppied()
    {
        return current_behaviour != null;
    }

}

public enum EquipableType
{
    oneHand = 0,
    twoHand = 1,
    head = 2,
    chest = 3,
    pants = 4,
    back = 5,
    foots = 6,
    space1 = 7,
    space2 = 8,
    ring1 = 9,
    ring2 = 10,
    neck = 11
}
