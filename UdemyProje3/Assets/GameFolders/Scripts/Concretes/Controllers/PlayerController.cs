using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Abstracts.Inputs;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Animations;
using UdemyProject3.Movements;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class PlayerController : MonoBehaviour , IEntityController
    {
        [Header("Movement Informations")] [SerializeField]
        float _moveSpeed = 10f;
        [SerializeField] float _turnSpeed = 10f;
        [SerializeField] Transform _turnTransform;
        
        IInputReader _input;
        IMover _mover;
        IRotator _xRotator;
        IRotator _yRotator;
        CharacterAnimation _animation;
        InventoryController _inventory;

        Vector3 _direction;

        public Transform TurnTransform => _turnTransform;

        void Awake()
        {
            _input = GetComponent<IInputReader>();
            _mover = new MoveWithCharacterController(this);
            _animation = new CharacterAnimation(this);
            _xRotator = new RototorX(this);
            _yRotator = new RotatorY(this);
            _inventory = GetComponent<InventoryController>();
        }

        void Update()
        {
            _direction = _input.Direction;

            _xRotator.RotationAction(_input.Rotation.x,_turnSpeed);
            _yRotator.RotationAction(_input.Rotation.y,_turnSpeed);

            if (_input.IsAttackButtonPress)
            {
                _inventory.CurrentWeapon.Attack();
            }

            if (_input.IsInventoryButtonPressed)
            {
                _inventory.ChangeWeapon();
            }
        }

        void FixedUpdate()
        {
            _mover.MoveAction(_direction,_moveSpeed);
            
        }

        void LateUpdate()
        {
            _animation.MoveAnimation(_direction.magnitude);
            _animation.AttackAnimation(_input.IsAttackButtonPress);
        }
    }
}