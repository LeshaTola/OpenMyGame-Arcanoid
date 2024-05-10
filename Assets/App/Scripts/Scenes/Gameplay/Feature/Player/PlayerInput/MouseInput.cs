using System;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Player.PlayerInput
{
    public class MouseInput : IInput
    {
        public event Action OnStartInput;
        public event Action OnEndInput;

        private Camera mainCamera;

        public MouseInput(Camera mainCamera)
        {
            this.mainCamera = mainCamera;
        }

        public Vector2 GetPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnStartInput?.Invoke();
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnEndInput?.Invoke();
            }
            
            if (Input.GetMouseButton(0))
            {
                return mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            return default;
        }
    }
}