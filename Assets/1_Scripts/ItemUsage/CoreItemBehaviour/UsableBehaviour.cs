using UnityEngine;
public enum UseResult
{
    Success,
    Fail,
    Consume
}
public abstract class UsableBehaviour : ItemBehaviour
{
    public UseResult Use(int ID)
    {
        return OnUse(ID);
    }
    public abstract UseResult OnUse(int ID);

}
