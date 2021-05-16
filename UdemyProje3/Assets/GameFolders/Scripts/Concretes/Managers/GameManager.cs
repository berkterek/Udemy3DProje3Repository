using System.Collections;
using UdemyProject3.Abstracts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UdemyProject3.Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [SerializeField] float _waitNextLevel = 10f;
        [SerializeField] float _waveMultiple = 1.2f;
        [SerializeField] int _maxWaveBoundaryCount = 50;

        int _currentWaveMaxCount;
        
        public bool IsWaveFinished => _currentWaveMaxCount <= 0;
        
        void Awake()
        {
            SetSingletonThisGameObject(this);
        }

        void Start()
        {
            _currentWaveMaxCount = _maxWaveBoundaryCount;
        }

        public void LoadLevel(string name)
        {
            StartCoroutine(LoadLevelAsync(name));
        }

        private IEnumerator LoadLevelAsync(string name)
        {
            yield return SceneManager.LoadSceneAsync(name);
        }

        public void DecreaseWaveCount()
        {
            if (IsWaveFinished)
            {
                if (EnemyManager.Instance.IsListEmpty)
                {
                    StartCoroutine(StartNextWaveAsync());    
                }
            }
            else
            {
                _currentWaveMaxCount--;    
            }
        }

        private IEnumerator StartNextWaveAsync()
        {
            yield return new WaitForSeconds(_waitNextLevel);
            _maxWaveBoundaryCount = System.Convert.ToInt32(_maxWaveBoundaryCount * _waveMultiple);
            _currentWaveMaxCount = _maxWaveBoundaryCount;
        }
    }    
}