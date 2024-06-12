using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb
{
	public struct Line
	{
		public Vector2Int Direction;
		[Min(-1)] public int iterations;
	}
}
