using System.Collections.Generic;
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
    [SerializeField] UIContainer uiContainer_playerInventory;
    [SerializeField] UIContainer uiContainer_chest;
    [SerializeField] GameObject scope;

    [Header("UI Component System")]
    [SerializeField] List<UIComponent> uIComponents = new List<UIComponent>();

    public override void SingletonAwake()
    {

    }

    public static UISlot model_ui_slots { get { return Instance.uislots; } }
    public static TextMeshProUGUI Txt_InteractInfo { get { return Instance.txt_interactInfo; } }
    public static UIContainer UIContainer_PlayerInventory { get { return Instance.uiContainer_playerInventory; } }
    public static UIContainer UIContainer_Chest { get { return Instance.uiContainer_chest; } }
    public static void TurnOnScope(bool _on) { Instance.scope.SetActive(_on);  }

    #region UI Component System
    public static void CloseAllUIComponents() { Instance._closeAllUIComponents(); }
    public static void AddUiComponent(UIComponent _component) { Instance._addUiComponent(_component); }
    public static bool RemoveUIComponent(UIComponent _component) { return Instance._removeUiComponent(_component); }
    void _addUiComponent(UIComponent _component) 
    {
        if (_component == null) return;
        if (uIComponents == null) uIComponents = new List<UIComponent>();
        if (!uIComponents.Contains(_component)) uIComponents.Add(_component);
    }
    bool _removeUiComponent(UIComponent _component) 
    {
        if (_component == null || uIComponents == null) return false;
        return uIComponents.Remove(_component);
    }
    void _closeAllUIComponents()
    {
        if (uIComponents == null || uIComponents.Count == 0) return;

        // Iterar sobre una copia para evitar problemas si Close() modifica la lista
        var snapshot = uIComponents.ToArray();
        foreach (var component in snapshot)
        {
            component?.Close();
        }

        uIComponents.Clear();
    }
    #endregion
}
