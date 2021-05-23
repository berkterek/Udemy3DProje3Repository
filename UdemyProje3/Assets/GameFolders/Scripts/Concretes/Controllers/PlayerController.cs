using System;
using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Combats;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Abstracts.Inputs;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Animations;
using UdemyProject3.Managers;
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

        [Header("Uis")] 
        [SerializeField] GameObject _gameOverPanel;
        
        IInputReader _input;
        IRotator _xRotator;
        IRotator _yRotator;
        IMover _mover;
        IHealth _health;
        CharacterAnimation _animation;
        InventoryController _inventory;

        Vector3 _direction;

        public Transform TurnTransform => _turnTransform;

        void Awake()
        {
            _input = GetComponent<IInputReader>();
            _health = GetComponent<IHealth>();
            _mover = new MoveWithCharacterController(this);
            _animation = new CharacterAnimation(this);
            _xRotator = new RototorX(this);
            _yRotator = new RotatorY(this);
            _inventory = GetComponent<InventoryController>();
        }

        void OnEnable()
        {
            _health.OnDead += () =>
            {
                _animation.DeadAnimation("death");
                _gameOverPanel.SetActive(true);
            };
            
            EnemyManager.Instance.Targets.Add(this.transform);
        }

        void OnDisable()
        {
            EnemyManager.Instance.Targets.Remove(this.transform);
        }

        void Update()
        {
            if (_health.IsDead) return;
            
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
            if (_health.IsDead) return;
            
            _mover.MoveAction(_direction,_moveSpeed);
        }

        void LateUpdate()
        {
            if (_health.IsDead) return;
            
            _animation.MoveAnimation(_direction.magnitude);
            _animation.AttackAnimation(_input.IsAttackButtonPress);
        }
    }
}