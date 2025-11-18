using TMPro;
using UnityEngine;

public class PlayerPanel : MonoSingleton<PlayerPanel>
{
    [SerializeField] CanvasGroup group;
    [SerializeField] float transitionTime = 0.2f;
    CanvasGroupSwitcher switcher;
    PanelFSM fsm;

    [SerializeField] TextMeshProUGUI txt_debug;

    public bool startOpen = false;

    public override void SingletonAwake()
    {
        switcher = new CanvasGroupSwitcher(group, this, startOpen, transitionTime);

        fsm = new PanelFSM(
            startOpen: startOpen, 
            switcher: switcher, 
            onOpenSucess: OnOpenSucess, 
            onCloseSucess: OnCloseSucess, 
            onSelectSucess: OnSelectSucess,
            s => txt_debug.text = s);
        
    }

    private void Start()
    {
    }

    public static void Open() => Instance.fsm.Open();
    public static void Close() => Instance.fsm.Close();
    public static void Wait() => Instance.fsm.Wait();

    public void OnOpenSucess()
    {

    }

    public void OnCloseSucess()
    {

    }

    public void OnSelectSucess()
    {

    }
   

}
