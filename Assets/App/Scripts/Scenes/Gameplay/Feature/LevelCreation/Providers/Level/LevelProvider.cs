using Scenes.Gameplay.Feature.Blocks;
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

		public LevelInfo GetCurrentLevelStateInfo()
		{
			return new LevelInfo()
			{
				Height = levelInfo.Height,
				Width = levelInfo.Width,
				BlocksMatrix = GetBlocksMatrix(),
				BonusesMatrix = levelInfo.BonusesMatrix,
			};
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
