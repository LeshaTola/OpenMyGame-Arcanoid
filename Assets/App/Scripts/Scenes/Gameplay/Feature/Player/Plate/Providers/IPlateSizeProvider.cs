using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Providers
{
	public interface IPlateSizeProvider
	{
		public Vector2 RightPosition { get; }
		public Vector2 LeftPosition { get; }
		public Vector2 CenterPosition { get; }
		public float Width { get; }

		public bool InBounds(Vector2 position);

	}
}
