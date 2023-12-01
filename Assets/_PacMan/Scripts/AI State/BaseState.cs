namespace _PacMan.Scripts.AI_State
{
    public interface BaseState
    {
        public void EnterState(Enemy enemy);

        public void UpdateState(Enemy enemy);

        public void ExitState(Enemy enemy);
    }
}