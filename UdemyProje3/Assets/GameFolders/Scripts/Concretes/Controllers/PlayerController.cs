﻿using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Inputs;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Movements;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Informations")] [SerializeField]
        float _moveSpeed = 10f;
        
        IInputReader _input;
        IMover _mover;

        Vector3 _direction;

        void Awake()
        {
            _input = GetComponent<IInputReader>();
            _mover = new MoveWithCharacterController(this);
        }

        void Update()
        {
            _direction = _input.Direction;
        }

        void FixedUpdate()
        {
            _mover.MoveAction(_direction,_moveSpeed);
        }
    }
}