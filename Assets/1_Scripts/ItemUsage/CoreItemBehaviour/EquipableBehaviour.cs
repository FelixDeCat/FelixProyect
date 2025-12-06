
public abstract class EquipableBehaviour : ItemBehaviour
{
    public EquipableType Type;

    public void Equip(int ID)
    {
        OnEquip(ID);
    }

    public void UnEquip()
    {
        OnUnEquip();
    }

    protected abstract void OnEquip(int ID);
    protected abstract void OnUnEquip();


}
