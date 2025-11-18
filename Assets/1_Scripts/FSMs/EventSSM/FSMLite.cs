namespace AI.Tools
{
    public class FSMLite
    {
        IState current;

        public FSMLite(IState first)
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
            return true;
        }
        public void UpdateFSM()
        {
            if (current != null)
            {
                current.Update();
            }
        }
    }
}
