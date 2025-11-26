using UnityEngine;

public abstract class EquipableBehaviour : UsabeBehaviour, IEquipable
{
    [SerializeField] protected EquipableType type;
    EquipableType IEquipable.Type { get => type; }

    public override UseResult OnUse(int ID)
    {
        // nothing
        return UseResult.Fail;
    }

    void IEquipable.Equip(int ID)
    {
        OnEquip(ID);
    }

    void IEquipable.UnEquip()
    {
        OnUnEquip();
    }

    protected abstract void OnEquip(int ID);
    protected abstract void OnUnEquip();


}
