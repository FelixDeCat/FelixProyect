using UnityEngine;
using UnityEngine.Pool;

public class UIGlobalData : MonoSingleton<UIGlobalData>
{
    [SerializeField] UISlot uislots;
    public static UISlot model_ui_slots { get { return Instance.uislots; } }

    public override void SingletonAwake()
    {
        
    }
}
