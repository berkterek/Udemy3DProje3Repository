using UdemyProject3.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UdemyProject3.Uis
{
    public class PlayerAddButton : MonoBehaviour
    {
        Button _button;
        
        void Awake()
        {
            _button = GetComponent<Button>();
        }

        void OnEnable()
        {
            _button.onClick.AddListener(OnButtonCliced);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonCliced);
        }
        
        void OnButtonCliced()
        {
            GameManager.Instance.IncreasePlayerCount();
        }
    }    
}

