using UnityEngine;

namespace _PacMan.Scripts.AI_State
{
    public class RetreatState : BaseState
    {
        public void EnterState(Enemy enemy)
        {
            Debug.Log("Start Retreating");
        }

        public void UpdateState(Enemy enemy)
        {
            if (enemy.Player != null)
            {
                enemy._navMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
            }
        }

        public void ExitState(Enemy enemy)
        {
            Debug.Log("Stop Retreating");
        }
    }
}