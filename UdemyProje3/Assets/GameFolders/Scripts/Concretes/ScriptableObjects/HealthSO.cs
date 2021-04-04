using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdemyProject3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Health Info", menuName = "Health Information/Create New", order = 51)]
    public class HealthSO : ScriptableObject
    {
        [SerializeField] int _maxHealth;

        public int MaxHealth => _maxHealth;
    }
}