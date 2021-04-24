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
    public class EnemyController : MonoBehaviour, IEntityController
    {
        IHealth _health;
        CharacterAnimation _animation;
        NavMeshAgent _navMeshAgent;
        InventoryController _inventoryController;
        StateMachine _stateMachine;

        Transform _playerTransform;
        bool _canAttack;
        
        public IMover Mover { get; private set; }

        public bool CanAttack =>
            Vector3.Distance(_playerTransform.position, this.transform.position) <= _navMeshAgent.stoppingDistance &&
            _navMeshAgent.velocity == Vector3.zero;

        void Awake()
        {
            Mover = new MoveWithNavMesh(this);
            _animation = new CharacterAnimation(this);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<IHealth>();
            _inventoryController = GetComponent<InventoryController>();
            _stateMachine = new StateMachine();
        }

        void Start()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;

            ChaseState chaseState = new ChaseState(this,_playerTransform);
            AttackState attackState = new AttackState();
            DeadState deadState = new DeadState();

            _stateMachine.AddState(chaseState, attackState, () => CanAttack);
            _stateMachine.AddState(attackState, chaseState, () => !CanAttack);
            _stateMachine.AddAnyState(deadState, () => _health.IsDead);
            
            _stateMachine.SetState(chaseState);
        }

        void Update()
        {
            if (_health.IsDead) return;

            _stateMachine.Tick();
        }

        void FixedUpdate()
        {
            if (_canAttack)
            {
                _inventoryController.CurrentWeapon.Attack();
            }
        }

        void LateUpdate()
        {
            _animation.MoveAnimation(_navMeshAgent.velocity.magnitude);
            _animation.AttackAnimation(_canAttack);
        }
    }
}