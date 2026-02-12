using System;
using UnityEngine;

namespace AI.Tools
{
    public class FSMLite
    {
        IState current;

        Action<string> onChange;

        public FSMLite(IState first, Action<string> _onChange = null)
        {
            current = first;
            onChange = _onChange;
        }

        public void StartFSM()
        {
            if (current == null) throw new System.Exception("No hay un Estado marcado como FIRST");
            current.Enter();
            DebugState(current);
        }

        public bool ChangeTo(IState next)
        {
            if (current == null) return false;
            current.Exit();
            current = next;
            current.Enter();

            DebugState(current);

            return true;
        }
        public void UpdateFSM()
        {
            if (current != null)
            {
                current.Update();
            }
        }

        void DebugState(IState state)
        {
            var estate = current as EState;
            onChange?.Invoke(estate.ToString());
        }
    }
}
