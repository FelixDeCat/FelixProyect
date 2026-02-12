using TMPro;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour
{
    [Header("UIComponent")]
    [SerializeField] protected CanvasGroup group;

    [SerializeField] bool interactable = false;


    [SerializeField] float transitionTime = 0.2f;
    CanvasGroupSwitcher switcher;
    protected PanelFSM fsm;

    [SerializeField] TextMeshProUGUI txt_debug;

    public bool startOpen = false;

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
    public void Close(bool removeFromManager = false)
    {
        if (removeFromManager)
        {
            UIGlobalData.RemoveUIComponent(this);
        }
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
