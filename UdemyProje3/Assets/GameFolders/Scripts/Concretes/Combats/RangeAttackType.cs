using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Combats;
using UdemyProject3.Managers;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Combats
{
    public class RangeAttackType : IAttackType
    {
        AttackSO _attackSo;
        Camera _camera;
        
        public RangeAttackType(Transform transformObject,AttackSO attackSo)
        {
            _camera = transformObject.GetComponent<Camera>();
            _attackSo = attackSo;
        }
        
        public void AttackAction()
        {
            Ray ray = _camera.ViewportPointToRay(Vector3.one / 2f);
            
            if (Physics.Raycast(ray,out RaycastHit hit,_attackSo.FloatValue,_attackSo.LayerMask))
            {
                if (hit.collider.TryGetComponent(out IHealth health))
                {
                    health.TakeDamage(_attackSo.Damage);
                }
            }
            
            SoundManager.Instance.RangeAttackSound(_attackSo.Clip,_camera.transform.position);
        }
    }    
}