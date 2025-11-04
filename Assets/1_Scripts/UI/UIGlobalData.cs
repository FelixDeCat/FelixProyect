using TMPro;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Manager General de referencias UI
/// </summary>

public class UIGlobalData : MonoSingleton<UIGlobalData>
{
    [SerializeField] UISlot uislots;
    [SerializeField] TextMeshProUGUI txt_interactInfo;
    public static UISlot model_ui_slots { get { return Instance.uislots; } }
    public static TextMeshProUGUI Txt_InteractInfo { get { return Instance.txt_interactInfo; } }

    public override void SingletonAwake()
    {
         
    }
}
