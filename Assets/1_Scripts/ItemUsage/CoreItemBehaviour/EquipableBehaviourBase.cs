
public abstract class EquipableBehaviourBase : ItemBehaviour
{
    public EquipableType Type;

    EquipSlot slot_data;

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
