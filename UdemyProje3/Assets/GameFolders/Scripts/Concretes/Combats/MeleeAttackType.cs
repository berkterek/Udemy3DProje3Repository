using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Combats;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Combats
{
    public class MeleeAttackType : IAttackType
    {
        Transform _transformObject;
        AttackSO _attackSo;
        
        public MeleeAttackType(Transform transformObject, AttackSO attackSo)
        {
            _transformObject = transformObject;
            _attackSo = attackSo;
        }
        
        public void AttackAction()
        {
            Vector3 attackPoint = _transformObject.position;
            Collider[] colliders = Physics.OverlapSphere(attackPoint, _attackSo.FloatValue, _attackSo.LayerMask);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IHealth health))
                {
                    health.TakeDamage(_attackSo.Damage);
                }
            }
        }
    }    
}

