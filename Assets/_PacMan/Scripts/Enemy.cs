using System;
using System.Collections;
using System.Collections.Generic;
using _PacMan.Scripts.AI_State;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private BaseState _currentState;
    [SerializeField]
    public float _chaseDistance;
    [SerializeField]
    public PlayerController Player;
    [SerializeField]
    public List<Transform> _waypoints = new List<Transform>();
    
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();
    
    [HideInInspector]
    public NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _currentState = PatrolState;
        _currentState.EnterState(this);

        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if(Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
    
    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }
    
    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }
    
}
