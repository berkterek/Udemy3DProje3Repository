using System;
using System.Collections;
using System.Collections.Generic;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Helpers
{
    public class MeleeAttackRangeDisplay : MonoBehaviour
    {
        [SerializeField] float _radius = 1f;
        
        void OnDrawGizmos()
        {
            OnDrawGizmosSelected();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position,_radius);
        }
    }    
}

