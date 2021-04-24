using System;
using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Combats;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Animations;
using UdemyProject3.Movements;
using UdemyProject3.States.EnemyStates;
using UnityEngine;
using UnityEngine.AI;

namespace UdemyProject3.Controllers
{
    public class EnemyController : MonoBehaviour, IEnemyController
    {
        IHealth _health;
        NavMeshAgent _navMeshAgent;
        StateMachine _stateMachine;

        bool _canAttack;
        
        public IMover Mover { get; private set; }
        public InventoryController Inventory { get; private set; }
        public CharacterAnimation Animation { get; private set; }
        public Transform Target { get; set; }
        public float Magnitude => _navMeshAgent.velocity.magnitude;

        public bool CanAttack =>
            Vector3.Distance(Target.position, this.transform.position) <= _navMeshAgent.stoppingDistance &&
            _navMeshAgent.velocity == Vector3.zero;

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<IHealth>();
            _stateMachine = new StateMachine();
            
            Mover = new MoveWithNavMesh(this);
            Animation = new CharacterAnimation(this);
            Inventory = GetComponent<InventoryController>();
        }

        void Start()
        {
            Target = FindObjectOfType<PlayerController>().transform;

            ChaseState chaseState = new ChaseState(this);
            AttackState attackState = new AttackState(this);
            DeadState deadState = new DeadState();

            _stateMachine.AddState(chaseState, attackState, () => CanAttack);
            _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
            _stateMachine.AddAnyState(deadState, () => _health.IsDead);
            
            _stateMachine.SetState(chaseState);
        }

        void Update()
        {
            _stateMachine.Tick();
        }

        void FixedUpdate()
        {
            _stateMachine.TickFixed();
        }

        void LateUpdate()
        {
            _stateMachine.TickLate();
        }
    }
}