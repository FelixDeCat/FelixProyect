using UnityEngine;



public class ConsumibleBase : UsabeBehaviour
{
    public override UseResult OnUse(int ID)
    {
        ItemData data = InventoryDataCenter.DataBase[ID];

        foreach (var c in data.Commands)
        {
            switch (c.KeyToAffect)
            {
                case KeyToAffect.life:
                    CustomConsole.LogPass("Me curo: " + c.value + " de vida");
                    break;
                case KeyToAffect.energy:
                    break;
                case KeyToAffect.speed:
                    break;
                case KeyToAffect.strenght:
                    break;
                case KeyToAffect.mana:
                    break;
                case KeyToAffect.command:
                    break;
            }
        }



        CustomConsole.LogPass("Se uso un Item");
        return UseResult.Consume;
    }
}
