using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Combats;
using UdemyProject3.Combats;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] bool _canFire;
        [SerializeField] Transform _transformObject;
        [SerializeField] AttackSO _attackSo;

        float _currentTime = 0f;
        IAttackType _attackType;

        void Awake()
        {
            _attackType = _attackSo.GetAttackType(_transformObject);
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            _canFire = _currentTime > _attackSo.AttackMaxDelay;
        }

        public void Attack()
        {
            if (!_canFire) return;
            
            _attackType.AttackAction();
            
            _currentTime = 0f;
        }
    }    
}

