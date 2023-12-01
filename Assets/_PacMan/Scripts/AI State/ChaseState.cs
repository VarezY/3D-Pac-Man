using UnityEngine;

namespace _PacMan.Scripts.AI_State
{
    public class ChaseState : BaseState
    {
        public void EnterState(Enemy enemy)
        {
            Debug.Log("Start Chasing");
        }

        public void UpdateState(Enemy enemy)
        {

            if (enemy.Player != null)
            {
                enemy._navMeshAgent.destination = enemy.Player.transform.position;
                if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) > enemy._chaseDistance)
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