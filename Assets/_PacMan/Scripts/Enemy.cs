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
    public float chaseDistance;
    [SerializeField]
    public PlayerController player;

    [SerializeField] private GameObject listWaypoints;
    
    [SerializeField]
    public List<Transform> waypoints = new List<Transform>();
    
    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();
    
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    [HideInInspector] 
    public Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        _currentState = PatrolState;
        _currentState.EnterState(this);
    }

    private void Start()
    {
        if(player != null)
        {
            player.OnPowerUpStart += StartRetreating;
            player.OnPowerUpStop += StopRetreating;
        }

        InitWaypoints();
    }

    private void InitWaypoints()
    {
        foreach(Transform child in listWaypoints.transform)
        {
            waypoints.Add(child);
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_currentState != RetreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().Dead();
            }
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
    
    public void Dead()
    {
        Destroy(gameObject);
    }
    
}
