using System;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player.PlayerInput
{
	public class MouseInput : IInput, ITickable
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
			if (Input.GetMouseButton(0))
			{
				return mainCamera.ScreenToWorldPoint(Input.mousePosition);
			}
			return default;
		}

		public void Tick()
		{
			if (Input.GetMouseButtonDown(0))
			{
				OnStartInput?.Invoke();
			}
			if (Input.GetMouseButtonUp(0))
			{
				OnEndInput?.Invoke();
			}
		}
	}
}