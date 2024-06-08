using Features.Saves.Gameplay.DTOs.Level;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Providers.Level
{
	public class LevelProvider : ILevelProvider
	{
		private Dictionary<Vector2Int, Block> blocks = new();
		private LevelInfo levelInfo;

		public LevelProvider()
		{
		}

		public Dictionary<Vector2Int, Block> Blocks { get => blocks; }
		public LevelInfo LevelInfo { get => levelInfo; }

		public void Init(Dictionary<Vector2Int, Block> blocks, LevelInfo levelInfo)
		{
			this.blocks = blocks;
			this.levelInfo = levelInfo;
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

		public LevelState GetLevelState()
		{
			return new LevelState()
			{
				levelInfo = new LevelInfo()
				{
					Height = levelInfo.Height,
					Width = levelInfo.Width,
					BlocksMatrix = GetBlocksMatrix(),
					BonusesMatrix = levelInfo.BonusesMatrix,
				},
				blockHealth = GetBlocksHealth()
			};
		}

		private Dictionary<Vector2, int> GetBlocksHealth()
		{
			Dictionary<Vector2, int> blocksHealth = new();
			foreach (var key in blocks.Keys)
			{
				if (blocks[key].Config.TryGetComponent(out HealthComponent healthComponent))
				{
					blocksHealth.Add(key, healthComponent.Health);
				}
			}
			return blocksHealth;
		}

		public void SetLevelState(LevelState levelState)
		{
			foreach (var key in blocks.Keys)
			{
				if (blocks[key].Config.TryGetComponent(out HealthComponent healthComponent))
				{
					healthComponent.SetHealth(levelState.blockHealth[key]);
				}
			}
		}

		private int[,] GetBlocksMatrix()
		{
			int[,] matrix = new int[levelInfo.Width, levelInfo.Height];

			for (int i = 0; i < levelInfo.Width; i++)
			{
				for (int j = 0; j < levelInfo.Height; j++)
				{
					matrix[i, j] = -1;
				}
			}

			foreach (var matrixPosition in blocks.Keys)
			{
				matrix[matrixPosition.x, matrixPosition.y] = levelInfo.BlocksMatrix[matrixPosition.x, matrixPosition.y];
			}

			return matrix;
		}
	}
}
