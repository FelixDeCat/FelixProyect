using UnityEngine;

public class ConsumibleBase : UsabeBehaviour
{
    public override UseResult OnUse()
    {
        CustomConsole.LogPass("Se uso un Item");
        return UseResult.Consume;
    }
}
