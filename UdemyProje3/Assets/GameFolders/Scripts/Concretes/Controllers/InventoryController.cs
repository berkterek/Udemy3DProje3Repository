using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] WeaponController[] _weapons;

        Animator _animator;
        
        public WeaponController CurrentWeapon { get; private set; }

        byte _index = 0;

        void Awake()
        {
            _weapons = GetComponentsInChildren<WeaponController>();

            foreach (WeaponController weapon in _weapons)
            {
                weapon.gameObject.SetActive(false);
            }
            
            _animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            CurrentWeapon = _weapons[_index];
            CurrentWeapon.gameObject.SetActive(true);
        }

        public void ChangeWeapon()
        {
            _index++;

            if (_index >= _weapons.Length)
            {
                _index = 0;
            }

            CurrentWeapon = _weapons[_index];

            foreach (WeaponController weapon in _weapons)
            {
                if (CurrentWeapon == weapon)
                {
                    weapon.gameObject.SetActive(true);
                    _animator.runtimeAnimatorController = CurrentWeapon.AttackSO.AnimatorOverride;
                }
                else
                {
                    weapon.gameObject.SetActive(false);    
                }
            }
        }
    }
}
