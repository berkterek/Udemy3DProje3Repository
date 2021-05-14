using UdemyProject3.Controllers;
using UnityEngine;

namespace UdemyProject3.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spawner Info",menuName = "Combat/Spawner Info/Create New")]
    public class SpawnInfoSO : ScriptableObject
    {
        [SerializeField] EnemyController _enemyPrefab;
        [SerializeField] float _maxSpawnTime = 15f;
        [SerializeField] float _minSpawnTime = 3f;

        public EnemyController EnemyPrefab => _enemyPrefab;
        public float RandomSpawnMaxTime => Random.Range(_minSpawnTime, _maxSpawnTime);
    }    
}

