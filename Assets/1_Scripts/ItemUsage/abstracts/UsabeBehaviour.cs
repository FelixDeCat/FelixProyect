using UnityEngine;

public abstract class UsabeBehaviour : MonoBehaviour, IUsable
{
    UseResult IUsable.Use(int ID)
    {
        return OnUse(ID);
    }
    public abstract UseResult OnUse(int ID);

    
}
