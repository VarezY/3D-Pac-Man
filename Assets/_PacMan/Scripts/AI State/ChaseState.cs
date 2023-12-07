using UnityEngine;

namespace _PacMan.Scripts.AI_State
{
    public class ChaseState : BaseState
    {
        private static readonly int AnimatorState = Animator.StringToHash("Chase State");

        public void EnterState(Enemy enemy)
        {
            Debug.Log("Start Chasing");
            enemy.animator.SetTrigger(AnimatorState);
        }

        public void UpdateState(Enemy enemy)
        {

            if (enemy.player != null)
            {
                enemy.navMeshAgent.destination = enemy.player.transform.position;
                if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) > enemy.chaseDistance)
                {
                    Debug.Log("Enemy Missing, Start patroling...");
                    enemy.SwitchState(enemy.PatrolState);
                }
            }
        }

        public void ExitState(Enemy enemy)
        {
            Debug.Log("Stop Chasing");
        }
    }
}