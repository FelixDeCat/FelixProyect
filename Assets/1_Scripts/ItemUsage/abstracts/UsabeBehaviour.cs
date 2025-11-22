using UnityEngine;

public abstract class UsabeBehaviour : MonoBehaviour, IUsable
{
    UseResult IUsable.Use()
    {
        return OnUse();
    }

    public abstract UseResult OnUse();
}
