using UnityEngine;



public class ConsumibleBase : UsableBehaviour
{
    public override UseResult OnUse(int ID)
    {
        ItemData data = InventoryDataCenter.DataBase[ID];

        CustomConsole.LogPass("Se uso un Item");
        return UseResult.Consume;
    }
}
