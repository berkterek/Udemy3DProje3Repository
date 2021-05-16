using UnityEngine;
using UnityEngine.UI;

namespace UdemyProject3.Abstracts.Uis
{
    public abstract class MyButton : MonoBehaviour
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

        protected abstract void HandleOnButtonClicked();
    }    
}

