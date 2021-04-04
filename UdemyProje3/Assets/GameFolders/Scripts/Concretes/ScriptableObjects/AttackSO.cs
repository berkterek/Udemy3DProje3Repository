using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdemyProject3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Attack Info",menuName = "Attack Information/Create New",order = 51)]
    public class AttackSO : ScriptableObject
    {
        [SerializeField] int _damage = 10;
        [SerializeField] float _floatValue = 1f;
        [SerializeField] LayerMask _layerMask;
        [SerializeField] float _attackMaxDelay = 0.25f;

        public int Damage => _damage;
        public float FloatValue => _floatValue;
        public LayerMask LayerMask => _layerMask;
        public float AttackMaxDelay => _attackMaxDelay;
    }    
}

