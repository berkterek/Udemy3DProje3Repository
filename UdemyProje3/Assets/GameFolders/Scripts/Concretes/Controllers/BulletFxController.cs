using UnityEngine;

namespace UdemyProject3.Controllers
{
    public class BulletFxController : MonoBehaviour
    {
        [SerializeField] float _maxLifeTime = 5f;
        [SerializeField] float _moveSpeed = 100f;

        Rigidbody _rigidbody;
        ParticleSystem _particleSystem;

        float _currentLifeTime = 0f;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        void Start()
        {
            _particleSystem.Play();
        }

        void Update()
        {
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime > _maxLifeTime)
            {
                Destroy(this.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Destroy(this.gameObject);
        }

        public void SetDirection(Vector3 direction)
        {
            _rigidbody.velocity = direction * _moveSpeed;
        }
    }    
}