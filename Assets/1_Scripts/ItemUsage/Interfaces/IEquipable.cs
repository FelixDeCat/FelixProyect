public interface IEquipable
{
    public EquipableType Type { get; }
    public void Equip(int ID);
    public void UnEquip();
}


