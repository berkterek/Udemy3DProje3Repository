using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Movements;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class EnemyController : MonoBehaviour , IEntityController
    {
        [SerializeField] Transform _playerPrefab;

        IMover _mover;

        void Awake()
        {
            _mover = new MoveWithNavMesh(this);
        }

        void Update()
        {
            _mover.MoveAction(_playerPrefab.transform.position,10f);
        }
    }    
}

