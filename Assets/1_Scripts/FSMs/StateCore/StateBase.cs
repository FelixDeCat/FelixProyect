using System.Runtime.CompilerServices;

namespace AI.Tools
{
    [System.Serializable]
    public abstract class StateBase : IState
    {

        void IState.Enter() => OnEnter();
        void IState.Exit() => OnExit();
        void IState.Update() => OnUpdate();

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
    }
}