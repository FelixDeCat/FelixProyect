namespace AI.Tools
{
    [System.Serializable]
    public abstract class StateBase : IState, IPausable
    {
        #region FSM States
        void IState.Enter() => OnEnter();
        void IState.Exit() => OnExit();
        void IState.Update() => OnUpdate();
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
        #endregion

        #region Pause & Resume
        void IPausable.Pause() { OnPause(); }
        void IPausable.Resume() { OnResume(); }
        protected virtual void OnPause() { }
        protected virtual void OnResume() { }
        #endregion
    }
}