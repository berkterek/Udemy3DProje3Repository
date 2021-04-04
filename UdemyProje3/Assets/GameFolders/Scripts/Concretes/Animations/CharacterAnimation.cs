using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Controllers;
using UdemyProject3.Controllers;
using UnityEngine;

namespace UdemyProject3.Animations
{
    public class CharacterAnimation
    {
        Animator _animator;

        public CharacterAnimation(IEntityController entity)
        {
            _animator = entity.transform.GetComponentInChildren<Animator>();
        }

        public void MoveAnimation(float moveSpeed)
        {
            if (_animator.GetFloat("moveSpeed") == moveSpeed) return;
            
            _animator.SetFloat("moveSpeed",moveSpeed,0.1f,Time.deltaTime);
        }
    }    
}

