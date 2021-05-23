using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UdemyProject3.Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _prefabs;
        
        PlayerInputManager _playerInputManager;
        int _playerIndex;

        void Awake()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();
            _playerInputManager.playerPrefab = _prefabs[_playerIndex];
        }

        void OnEnable()
        {
            StartCoroutine(LoadPlayersAsync());
        }

        IEnumerator LoadPlayersAsync()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
            
            for (int i = 0; i < GameManager.Instance.PlayerCount; i++)
            {
                _playerInputManager.JoinPlayer(_playerIndex);
                yield return waitForSeconds;
            }
        }

        public void HandleOnJoin()
        {
            _playerIndex++;

            if (_playerIndex >= _prefabs.Length) _playerIndex = _prefabs.Length - 1;

            _playerInputManager.playerPrefab = _prefabs[_playerIndex];
        }

        public void HandleOnLeft()
        {
            _playerIndex--;

            if (_playerIndex < 0) _playerIndex = 0;
            
            _playerInputManager.playerPrefab = _prefabs[_playerIndex];
        }
    }    
}