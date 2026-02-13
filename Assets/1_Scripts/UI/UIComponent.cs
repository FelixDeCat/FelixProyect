using TMPro;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour
{
    [Header("UIComponent")]
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float transitionTime = 0.2f;
    [SerializeField] private bool startOpen = false;

    protected PanelFSM fsm;
    private CanvasGroupSwitcher switcher;

    [SerializeField] private TextMeshProUGUI txt_debug;

    protected void CustomAwake()
    {
        switcher = new CanvasGroupSwitcher(
            group: group,
            owner: this,
            openOnStart: startOpen,
            time: transitionTime
            );

        fsm = new PanelFSM(
            startOpen: startOpen,
            switcher: switcher,
            onOpenSucess: EVENT_Open_Sucess,
            onCloseSucess: EVENT_Close_Sucess,
            onSelectSucess: EVENT_Select_Sucess,
            stringDEBUG => 
            { 
                if (txt_debug != null) 
                    txt_debug.text = stringDEBUG; 
            });
    }


    public void Open()
    {
        UIGlobalData.AddUiComponent(this);
        fsm.Open();
    }
    public void Close()
    {
        UIGlobalData.RemoveUIComponent(this);
        UIGlobalData.CloseAllUIComponents();
        fsm.Close();
    }


    protected virtual void EVENT_Open_Sucess()
    {

    }
    protected virtual void EVENT_Close_Sucess()
    {

    }
    protected virtual void EVENT_Select_Sucess()
    {

    }
}
