using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _PacMan.Scripts.AI_State
{
    public class PatrolState : BaseState
    {
        private bool _isMoving;
        private Vector3 _destination;
        private static readonly int AnimatorState = Animator.StringToHash("Patrol State");

        
        public void EnterState(Enemy enemy)
        {
            Debug.Log("Start Patrol");
            enemy.animator.SetTrigger(AnimatorState);
            _isMoving = false;
        }

        public void UpdateState(Enemy enemy)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                Debug.Log("Moving to Patrol one of Waypoints");

                int index = Random.Range(0, enemy.waypoints.Count);
                _destination = enemy.waypoints[index].position;

                enemy.navMeshAgent.destination = _destination;
            }
            else
            {
                if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) <= enemy.chaseDistance)
                {
                    Debug.Log("Player Detected, switch mode to chase");
                    enemy.SwitchState(enemy.ChaseState);
                }
                else if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
                {
                    Debug.Log("Reach Patrol Waypoint");
                    _isMoving = false;
                }
            }
        }

        public void ExitState(Enemy enemy)
        {
            Debug.Log("Stop Patrol");
        }
    }
}