using UnityEngine;

public class EquipableBase : EquipableBehaviour
{
    [SerializeField] GameObject current;
    string currentName;

    Transform currentParent;
    protected async override void OnEquip(int ID)
    {
        var item = InventoryDataCenter.Get_ItemData_ByID(ID);
        if (item == null) throw new System.Exception("Item no se encuentra");

        currentName = item.Name;
        current = await InstanceResourceManager.FindAndPoolObject(path: "Equipables", name: currentName);
        current.name = currentName;
        
        switch (item.Equipable.Type)
        {
            case EquipableType.oneHand:
                currentParent = EquipDataManager.RightHand;
                
                break;
            case EquipableType.twoHand:
                break;
            case EquipableType.head:
                break;
            case EquipableType.chest:
                break;
            case EquipableType.pants:
                break;
            case EquipableType.back:
                break;
            case EquipableType.foots:
                break;
            case EquipableType.space1:
                break;
            case EquipableType.space2:
                break;
            case EquipableType.ring1:
                break;
            case EquipableType.ring2:
                break;
            case EquipableType.neck:
                break;
        }

        current.transform.SetParent(currentParent);
        current.transform.position = EquipDataManager.RightHand.position;
        current.transform.localRotation = Quaternion.identity;

    }

    protected override void OnUnEquip()
    {
        InstanceResourceManager.Release(current.name, current);
    }
}