using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.States;
using UnityEngine;

namespace UdemyProject3.States.EnemyStates
{
    public class AttackState : IState
    {
        public void OnEnter()
        {
            Debug.Log($"{nameof(AttackState)} {nameof(OnEnter)}");
        }
        
        public void OnExit()
        {
            Debug.Log($"{nameof(AttackState)} {nameof(OnExit)}");
        }

        public void Tick()
        {
            // Debug.Log(nameof(AttackState));
        }
    }    
}
