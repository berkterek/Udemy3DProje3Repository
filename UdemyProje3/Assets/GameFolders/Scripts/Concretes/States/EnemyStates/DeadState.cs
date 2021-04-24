using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.States;
using UnityEngine;

namespace UdemyProject3.States.EnemyStates
{
    public class DeadState : IState
    {
        public void OnEnter()
        {
            // Debug.Log($"{nameof(DeadState)} {nameof(OnEnter)}");
        }
        
        public void OnExit()
        {
            // Debug.Log($"{nameof(DeadState)} {nameof(OnExit)}");
        }
        
        public void Tick()
        {
            // Debug.Log(nameof(DeadState));
        }
    }    
}
