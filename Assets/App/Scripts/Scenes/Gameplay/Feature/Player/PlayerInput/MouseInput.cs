using System;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Player.PlayerInput
{
	public class MouseInput : IInput, ITickable
	{
		public event Action<Vector2> OnStartInput;
		public event Action<Vector2> OnEndInput;

		private Camera mainCamera;

		public MouseInput(Camera mainCamera)
		{
			this.mainCamera = mainCamera;
		}

		public bool IsActive { get; set; } = true;

		public Vector2 GetPosition()
		{
			if (!IsActive)
			{
				return default;
			}

			if (Input.GetMouseButton(0))
			{
				return GetMousePosition();
			}
			return default;
		}

		public void Tick()
		{
			if (!IsActive)
			{
				return;
			}

			if (Input.GetMouseButtonDown(0))
			{
				OnStartInput?.Invoke(GetMousePosition());
			}
			if (Input.GetMouseButtonUp(0))
			{
				OnEndInput?.Invoke(GetMousePosition());
			}
		}

		private Vector2 GetMousePosition()
		{
			return mainCamera.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}