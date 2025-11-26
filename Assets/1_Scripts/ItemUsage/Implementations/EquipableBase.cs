using UnityEngine;

public class EquipableBase : EquipableBehaviour
{
    protected override void OnEquip(int ID)
    {
        switch (type)
        {
            case EquipableType.oneHand:
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
    }

    protected override void OnUnEquip()
    {
        
    }
}