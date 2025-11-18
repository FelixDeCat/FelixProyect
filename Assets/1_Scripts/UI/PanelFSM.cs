using System;
using UnityEngine;
using AI.Tools;

public enum UIPanelState
{
    Open,
    Close,
    Select,
    Wait,
    OpenComplete,
    CloseComplete
}
public class PanelFSM
{
    [SerializeField] CanvasGroupSwitcher switcher;

    [SerializeField] Closed closed;
    [SerializeField] Selected selected;
    [SerializeField] Waiting waiting;
    [SerializeField] OpeningTransition openingTransition;
    [SerializeField] ClosingTransition closeningTransition;

    FSM<UIPanelState> fsm;

    CanvasGroupSwitcher GetSwitcher() => switcher;

    public PanelFSM(bool startOpen, CanvasGroupSwitcher switcher, Action onOpenSucess, Action onCloseSucess, Action onSelectSucess, Action<string> debug = null)
    {
        waiting = new Waiting(onOpenSucess);
        closed = new Closed(onCloseSucess);
        selected = new Selected(onSelectSucess);
        openingTransition = new OpeningTransition(OnFinishOpenTransition);
        closeningTransition = new ClosingTransition(OnFinishCloseTransition);

        this.switcher = switcher;
        switcher.SetOpenOnStart(!startOpen); // lo invierto para que la FSM sepa si lo tiene que dar vuelta

        fsm = new FSM<UIPanelState>(startOpen ? waiting : closed);

        fsm.SetDebug(debug);

        fsm.AddTransition(closed, UIPanelState.Open, openingTransition);
        fsm.AddTransition(selected, UIPanelState.Wait, waiting);
        fsm.AddTransition(selected, UIPanelState.Close, closeningTransition);
        fsm.AddTransition(waiting, UIPanelState.Select, selected);
        fsm.AddTransition(waiting, UIPanelState.Close, closeningTransition);
        fsm.AddTransition(openingTransition, UIPanelState.OpenComplete, waiting);
        fsm.AddTransition(closeningTransition, UIPanelState.CloseComplete, closed);

        closed.SetInputCallback(SendInput);
        selected.SetInputCallback(SendInput);
        waiting.SetInputCallback(SendInput);
        openingTransition.SetInputCallback(SendInput);
        closeningTransition.SetInputCallback(SendInput);

        closed.SetSwitcher(GetSwitcher);
        selected.SetSwitcher(GetSwitcher);
        waiting.SetSwitcher(GetSwitcher);
        openingTransition.SetSwitcher(GetSwitcher);
        closeningTransition.SetSwitcher(GetSwitcher);

        fsm.StartFSM();
    }
    public void Open() => SendInput(UIPanelState.Open);
    public void Close() => SendInput(UIPanelState.Close);
    public void Select() => SendInput(UIPanelState.Select);
    public void Wait() => SendInput(UIPanelState.Wait);

    void OnFinishOpenTransition() => SendInput(UIPanelState.OpenComplete);
    void OnFinishCloseTransition() => SendInput(UIPanelState.CloseComplete);

    void SendInput(UIPanelState input)
    {
        fsm.SendInput(input);
    }

    class Closed : PanelState
    {
        public Closed(Action SucessAction) : base(SucessAction)
        {
        }

        protected override void OnEnter()
        {
            Switcher.Close(success);
        }

        public override string ToString()
        {
            return "Close";
        }
    }
    class Waiting : PanelState
    {
        public Waiting(Action SucessAction) : base(SucessAction)
        {
        }

        protected override void OnEnter()
        {
            Switcher.Open(success);
        }
        public override string ToString()
        {
            return "Waiting";
        }
    }

    class Selected : PanelState
    {
        public Selected(Action SucessAction) : base(SucessAction)
        {
        }

        protected override void OnEnter()
        {
            Switcher.Open(success);
        }
        public override string ToString()
        {
            return "Selected";
        }
    }


    class OpeningTransition : PanelState
    {
        public OpeningTransition(Action SucessAction) : base(SucessAction)
        {
        }

        protected override void OnEnter()
        {
            Switcher.Open(success);
        }

        public override string ToString()
        {
            return "trans>>Open";
        }
    }
    class ClosingTransition : PanelState
    {
        public ClosingTransition(Action SucessAction) : base(SucessAction)
        {
        }

        protected override void OnEnter()
        {
            Switcher.Close(success);
        }
        public override string ToString()
        {
            return "trans>>Close";
        }
    }

    class PanelState : IState

    {
        protected Action success;
        public PanelState(Action SucessAction)
        {
            this.success = SucessAction;
        }

        #region Get(property) Set(by Func) Switcher
        protected CanvasGroupSwitcher Switcher { get { return switcher.Invoke(); } }



        private Func<CanvasGroupSwitcher> switcher = delegate { return null; };
        public PanelState SetSwitcher(Func<CanvasGroupSwitcher> _switcher)
        {
            switcher = _switcher;
            return this;
        }
        #endregion

        protected Action<UIPanelState> SendInput;
        public void SetInputCallback(Action<UIPanelState> _SendInput)
        {
            SendInput = _SendInput;
        }

        #region IState Interface
        void IState.Enter()
        {
            OnEnter();
        }
        void IState.Exit()
        {
            OnExit();
        }
        void IState.Update()
        {
            OnUpdate();
        }
        #endregion

        #region Virtuals
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate() { }
        #endregion
    }
}


