using System.Collections.Generic;
using UdemyProject3.Abstracts.Helpers;
using UdemyProject3.Controllers;
using UnityEngine;

namespace UdemyProject3.Managers
{
    public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
    {
        [SerializeField] int _maxCountOnGame = 50;
        [SerializeField] List<EnemyController> _enemies;

        public List<Transform> Targets { get; private set; }
        public bool CanSpawn => _maxCountOnGame > _enemies.Count;
        public bool IsListEmpty => _enemies.Count <= 0;
        
        void Awake()
        {
            SetSingletonThisGameObject(this);
            _enemies = new List<EnemyController>();
            Targets = new List<Transform>();
        }

        public void AddEnemyController(EnemyController enemyController)
        {
            enemyController.transform.parent = this.transform;
            _enemies.Add(enemyController);
        }

        public void RemoveEnemyController(EnemyController enemyController)
        {
            _enemies.Remove(enemyController);
            GameManager.Instance.DecreaseWaveCount();
        }

        public void DestroyAllEnemies()
        {
            foreach (EnemyController enemyController in _enemies)
            {
                Destroy(enemyController.gameObject);
            }
            
            _enemies.Clear();
        }
    }
}