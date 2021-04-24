using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.States;
using UnityEngine;

namespace UdemyProject3.States.EnemyStates
{
    public class ChaseState : IState
    {
        public void OnEnter()
        {
            // Debug.Log($"{nameof(ChaseState)} {nameof(OnEnter)}");
        }
        
        public void OnExit()
        {
            // Debug.Log($"{nameof(ChaseState)} {nameof(OnExit)}");
        }

        public void Tick()
        {
            Debug.Log(nameof(ChaseState));   
        }
    }    
}

