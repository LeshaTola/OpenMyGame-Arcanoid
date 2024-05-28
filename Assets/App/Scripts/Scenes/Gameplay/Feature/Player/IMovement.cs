using UnityEngine;

namespace Scenes.Gameplay.Feature.Player
{
	public interface IMovement
	{
		public void Move(Vector2 moveDirection, float speedMultiplier = 1f);
		public void ApplyDrag();
	}
}
