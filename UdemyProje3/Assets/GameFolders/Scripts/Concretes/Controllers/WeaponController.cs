using UdemyProject3.Abstracts.Combats;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] bool _canFire;

        float _currentTime = 0f;
        IAttackType _attackType;
        public AnimatorOverrideController AnimatorOverride => _attackType.AttackInfo.AnimatorOverride;

        void Awake()
        {
            _attackType = GetComponent<IAttackType>();
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            _canFire = _currentTime > _attackType.AttackInfo.AttackMaxDelay;
        }

        public void Attack()
        {
            if (!_canFire) return;
            
            _attackType.AttackAction();
            
            _currentTime = 0f;
        }
    }    
}

