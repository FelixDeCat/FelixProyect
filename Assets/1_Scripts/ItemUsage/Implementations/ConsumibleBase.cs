using UnityEngine;



public class ConsumibleBase : UsableBehaviourBase
{
    public override UseResult OnUse(int ID)
    {
        ItemData data = InventoryDataCenter.Get_Data_ByID(ID);

        CustomConsole.LogPass("Se uso un Item");
        return UseResult.Consume;
    }
}
