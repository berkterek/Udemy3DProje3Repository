using System.Collections;
using System.Collections.Generic;
using UdemyProject3.Abstracts.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UdemyProject3.Inputs
{
    public class InputReader : MonoBehaviour,IInputReader
    {
        int _index;
        
        public Vector3 Direction { get; private set; }
        public Vector2 Rotation { get; private set; }
        public bool IsAttackButtonPress { get; private set; }
        public bool IsInventoryButtonPressed { get; private set; }

        public void OnMove(InputAction.CallbackContext context)
        {
             Vector2 oldDirection = context.ReadValue<Vector2>();
             Direction = new Vector3(oldDirection.x, 0f, oldDirection.y);
        }

        public void OnRotator(InputAction.CallbackContext context)
        {
            Rotation = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            IsAttackButtonPress = context.ReadValueAsButton();
        }

        public void OnInventoryPressed(InputAction.CallbackContext context)
        {
            if (IsInventoryButtonPressed && context.action.triggered) return;

            StartCoroutine(WaitOnFrameAsync());
        }

        IEnumerator WaitOnFrameAsync()
        {
            IsInventoryButtonPressed = true && _index % 2 == 0;
            yield return new WaitForEndOfFrame();
            IsInventoryButtonPressed = false;

            _index++;
        }
    }
}

