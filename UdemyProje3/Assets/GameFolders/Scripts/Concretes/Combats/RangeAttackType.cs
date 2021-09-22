using UdemyProject3.Abstracts.Combats;
using UdemyProject3.Controllers;
using UdemyProject3.Managers;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Combats
{
    public class RangeAttackType : MonoBehaviour, IAttackType
    {
        [SerializeField] AttackSO _attackSo;
        [SerializeField] Camera _camera;
        [SerializeField] BulletFxController _bulletFx;
        [SerializeField] Transform _bulletPoint;
        
        public AttackSO AttackInfo => _attackSo;

        public void AttackAction()
        {
            Ray ray = _camera.ViewportPointToRay(Vector3.one / 2f);

            if (Physics.Raycast(ray, out RaycastHit hit, _attackSo.FloatValue, _attackSo.LayerMask))
            {
                if (hit.collider.TryGetComponent(out IHealth health))
                {
                    health.TakeDamage(_attackSo.Damage);
                }
            }

            var bullet = Instantiate(_bulletFx, _bulletPoint.position, _bulletPoint.rotation);
            bullet.SetDirection(ray.direction);
            
            SoundManager.Instance.RangeAttackSound(_attackSo.Clip, _camera.transform.position);
        }
    }
}