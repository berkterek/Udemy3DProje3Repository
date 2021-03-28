using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Movements;
using UdemyProject3.Controllers;
using UnityEngine;

namespace UdemyProject3.Movements
{
    public class RotatorY : IRotator
    {
        Transform _transform;
        float _tilt;

        public RotatorY(PlayerController playerController)
        {
            _transform = playerController.TurnTransform;
        }
        
        public void RotationAction(float direction,float speed)
        {
            direction *= speed * Time.deltaTime;
            _tilt = Mathf.Clamp(_tilt - direction, -30f, 30);
            _transform.localRotation = Quaternion.Euler(_tilt,0f,0f);
        }
    }    
}

