using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Abstracts.States;
using UnityEngine;

namespace UdemyProject3.States.EnemyStates
{
    public class DeadState : IState
    {
        IEnemyController _enemyController;
        float _maxTime = 5f;
        float _currentTime = 0f;
        
        public DeadState(IEnemyController enemyController)
        {
            _enemyController = enemyController;
        }
        
        public void OnEnter()
        {
            Debug.Log($"{nameof(DeadState)} {nameof(OnEnter)}");
            
            _enemyController.Dead.DeadAction();
            _enemyController.Animation.DeadAnimation();
            _enemyController.transform.GetComponent<CapsuleCollider>().enabled = false;
        }

        public void OnExit()
        {
            Debug.Log($"{nameof(DeadState)} {nameof(OnExit)}");
        }
        
        public void Tick()
        {
            return;
        }
        
        public void TickFixed()
        {
            return;
        }

        public void TickLate()
        {
            return;
        }
    }    
}
