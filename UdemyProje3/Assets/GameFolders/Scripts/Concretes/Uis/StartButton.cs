using System;
using UdemyProject3.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UdemyProject3.Uis
{
    public class StartButton : MonoBehaviour
    {
        Button _button;

        void Awake()
        {
            _button = GetComponent<Button>();
        }

        void OnEnable()
        {
            _button.onClick.AddListener(HandleOnButtonClicked);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnButtonClicked);
        }

        private void HandleOnButtonClicked()
        {
            GameManager.Instance.LoadLevel("Game");
        }
    }    
}

