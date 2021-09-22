using System.Collections;
using System.Collections.Generic;
using UdemyProject3.ScriptableObjects;
using UnityEngine;

namespace UdemyProject3.Abstracts.Combats
{
    public interface IAttackType
    {
        void AttackAction();
        public AttackSO AttackInfo { get;}
    }    
}

