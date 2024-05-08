using UnityEngine;

namespace Scenes.Gameplay.Feature.PlayerInput
{
	public interface IInput
	{
		public Vector2 GetPosition();
	}

	public class MouseInput : IInput
	{
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
	}
}