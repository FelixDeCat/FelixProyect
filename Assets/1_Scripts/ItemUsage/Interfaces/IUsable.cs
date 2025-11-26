public enum UseResult
{
    Success,
    Fail,
    Consume,
    Equip
}
public interface IUsable
{
    public UseResult Use(int ID);
}