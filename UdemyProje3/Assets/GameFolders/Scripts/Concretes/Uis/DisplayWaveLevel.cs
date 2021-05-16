using TMPro;
using UdemyProject3.Managers;
using UnityEngine;

namespace UdemyProject3.Uis
{
    public class DisplayWaveLevel : MonoBehaviour
    {
        TMP_Text _levelText;

        void Awake()
        {
            _levelText = GetComponent<TMP_Text>();
        }

        void OnEnable()
        {
            GameManager.Instance.OnNextWave += HandleOnNextWave;
        }

        void OnDisable()
        {
            GameManager.Instance.OnNextWave -= HandleOnNextWave;
        }
        
        void HandleOnNextWave(int levelValue)
        {
            _levelText.text = levelValue.ToString();
        }
    }    
}

