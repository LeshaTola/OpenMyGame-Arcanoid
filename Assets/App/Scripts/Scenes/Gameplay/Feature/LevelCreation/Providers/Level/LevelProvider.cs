using Scenes.Gameplay.Feature.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Providers.Level
{
	public class LevelProvider : ILevelProvider
	{
		private Dictionary<Vector2Int, Block> blocks = new();

		public LevelProvider()
		{
		}

		public Dictionary<Vector2Int, Block> Blocks { get => blocks; }

		public void Init(Dictionary<Vector2Int, Block> blocks)
		{
			this.blocks = blocks;
		}

		public void TurnOffColliders()
		{
			foreach (var block in blocks.Values)
			{
				block.BoxCollider.isTrigger = true;
			}
		}

		public void TurnOnColliders()
		{
			foreach (var block in blocks.Values)
			{
				block.BoxCollider.isTrigger = false;
			}
		}
	}
}
