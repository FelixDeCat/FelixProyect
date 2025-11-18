namespace AI.Tools
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
    }
}

