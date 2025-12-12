namespace AI.Tools
{
    using UnityEngine;
    public abstract class MonoState : MonoBehaviour, IState
    {
        ///////////////////////////////
        ////  IState Implement
        ///////////////////////////////
        void IState.Enter() => OnEnter();
        void IState.Exit() => OnExit();
        void IState.Update() => OnUpdate();

        ///////////////////////////////
        ////  Protected Virtuals
        ///////////////////////////////
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate() { }

        
    }
}
