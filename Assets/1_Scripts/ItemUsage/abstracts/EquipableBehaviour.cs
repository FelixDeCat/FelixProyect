using UnityEngine;

public abstract class EquipableBehaviour : UsabeBehaviour, IEquipable
{
    [SerializeField] EquipableType type;
    EquipableType IEquipable.Type { get => type; }

    public override UseResult OnUse()
    {
        // nothing

        return UseResult.Fail;
    }

    void IEquipable.Equip()
    {
        OnEquip();
    }

    void IEquipable.UnEquip()
    {
        OnUnEquip();
    }

    protected abstract void OnEquip();
    protected abstract void OnUnEquip();


}
