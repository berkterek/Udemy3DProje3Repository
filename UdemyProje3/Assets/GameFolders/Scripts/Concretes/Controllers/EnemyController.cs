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
        IMover _mover;
        IHealth _health;
        CharacterAnimation _animation;
        NavMeshAgent _navMeshAgent;
        InventoryController _inventoryController;
        StateMachine _stateMachine;

        Transform _playerTransform;
        bool _canAttack;

        public bool CanAttack =>
            Vector3.Distance(_playerTransform.position, this.transform.position) <= _navMeshAgent.stoppingDistance &&
            _navMeshAgent.velocity == Vector3.zero;

        void Awake()
        {
            _mover = new MoveWithNavMesh(this);
            _animation = new CharacterAnimation(this);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _health = GetComponent<IHealth>();
            _inventoryController = GetComponent<InventoryController>();
            _stateMachine = new StateMachine();
        }

        void Start()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;

            AttackState attackState = new AttackState();
            ChaseState chaseState = new ChaseState();
            DeadState deadState = new DeadState();

            _stateMachine.AddState(chaseState, attackState, () => CanAttack == true);
            _stateMachine.AddState(attackState, chaseState, () => CanAttack == false);
            _stateMachine.AddAnyState(deadState, () => _health.IsDead);
            
            _stateMachine.SetState(attackState);
        }

        void Update()
        {
            if (_health.IsDead) return;

            Debug.Log(CanAttack);

            //_mover.MoveAction(_playerTransform.position, 10f);

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