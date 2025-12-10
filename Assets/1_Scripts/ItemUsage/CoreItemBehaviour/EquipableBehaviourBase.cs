
using System;
using UnityEngine;

public abstract class EquipableBehaviourBase : ItemBehaviour
{
    public EquipableType Type;

    EquipSlot slot_data;

    [SerializeField] protected bool hasVisuals = true;
    public bool HasVisuals
    {
        get => hasVisuals;
    }

    public EquipSlot GetSlotData()
    {
        return slot_data;
    }

    public void Equip(EquipSlot slot)
    {
        this.slot_data = slot;
        OnEquip(slot);
    }

    public void UnEquip()
    {
        OnUnEquip();
    }

    protected abstract void OnEquip(EquipSlot slot);
    protected abstract void OnUnEquip();


}
