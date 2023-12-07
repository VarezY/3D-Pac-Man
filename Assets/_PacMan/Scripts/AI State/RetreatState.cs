using UnityEngine;

namespace _PacMan.Scripts.AI_State
{
    public class RetreatState : BaseState
    {
        private static readonly int AnimatorState = Animator.StringToHash("Retreat State");

        public void EnterState(Enemy enemy)
        {
            Debug.Log("Start Retreating");
            enemy.animator.SetTrigger(AnimatorState);
        }

        public void UpdateState(Enemy enemy)
        {
            if (enemy.player != null)
            {
                enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;
            }
        }

        public void ExitState(Enemy enemy)
        {
            Debug.Log("Stop Retreating");
        }
    }
}