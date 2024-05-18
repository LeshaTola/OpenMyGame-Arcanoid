using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.PlayerInput
{
	public interface IInput
	{
		public event Action<Vector2> OnStartInput;
		public event Action<Vector2> OnEndInput;

		public Vector2 GetPosition();
	}
}