using System;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Player.PlayerInput
{
	public interface IInput
	{
		public event Action OnStartInput;
		public event Action OnEndInput;
		
		public Vector2 GetPosition();
	}
}