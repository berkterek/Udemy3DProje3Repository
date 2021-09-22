using UdemyProject3.Abstracts.Movements;
using UnityEngine;

namespace UdemyProject3.Movements
{
    public class RibRotator : IRotator
    {
        float _speed = 10f;
        Transform _transform;
        float _tilt;

        public RibRotator(Transform transform)
        {
            _transform = transform;
        }
        
        public void RotationAction(float direction, float speed)
        {
            direction *= _speed * Time.deltaTime;
            _tilt = Mathf.Clamp(_tilt - direction, -30f, 30f);

            _transform.Rotate(new Vector3(0f,0f,_tilt));
        }
    }
}