using System;
using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Animations;
using UdemyProject3.Movements;
using UnityEngine;
using UnityEngine.AI;

namespace UdemyProject3.Controllers
{
    public class EnemyController : MonoBehaviour , IEntityController
    {
        [SerializeField] Transform _playerPrefab;

        IMover _mover;
        CharacterAnimation _animation;
        NavMeshAgent _navMeshAgent;

        void Awake()
        {
            _mover = new MoveWithNavMesh(this);
            _animation = new CharacterAnimation(this);
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            _mover.MoveAction(_playerPrefab.transform.position,10f);
        }

        void LateUpdate()
        {
            _animation.MoveAnimation(_navMeshAgent.velocity.magnitude);
        }
    }    
}

