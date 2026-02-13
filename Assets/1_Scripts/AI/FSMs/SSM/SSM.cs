using UnityEngine;

namespace AI.Tools
{
    public class SSM
    {
        IState current;

        IFixedUpdateable physicUpdate;

        public SSM(IState first)
        {
            current = first;
        }

        public void StartFSM()
        {
            if (current == null) throw new System.Exception("No hay un Estado marcado como FIRST");
            current.Enter();
        }

        public bool ChangeTo(IState next)
        {
            if (current == null) return false;
            current.Exit();
            current = next;
            current.Enter();
            physicUpdate = current as IFixedUpdateable;
            return true;
        }
        public void UpdateFSM()
        {
            if (current != null)
            {
                current.Update();
            }
        }
        public void FixedUpdateFSM()
        {
            if (physicUpdate != null)
            {
                physicUpdate.FixedTick(Time.fixedDeltaTime);
            }
        }
    }
}
