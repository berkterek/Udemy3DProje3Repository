using System.Collections;
using UdemyProject3.Abstracts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UdemyProject3.Managers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        void Awake()
        {
            SetSingletonThisGameObject(this);
        }

        public void LoadLevel(string name)
        {
            StartCoroutine(LoadLevelAsync(name));
        }

        private IEnumerator LoadLevelAsync(string name)
        {
            yield return SceneManager.LoadSceneAsync(name);
        }
    }    
}

