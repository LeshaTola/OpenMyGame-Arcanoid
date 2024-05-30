using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.ColorBomb
{
	public class LineBombComponent : General.Component
	{
		[SerializeField] private float pauseBetweenExplosions;
		[SerializeField] private List<Vector2Int> steps;

		public override void Execute()
		{
			if (steps == null || steps.Count <= 0)
			{
				return;
			}

			Vector2Int matrixSize = GetMaxVector2Int(Block.Neighbors);
			List<List<Block>> blocksToDestroyLists = InitializeLists();

			SetBlocksToDestroy(matrixSize, blocksToDestroyLists);
			foreach (var list in blocksToDestroyLists)
			{
				DestroyBlocksAsync(list);
			}
		}

		private void SetBlocksToDestroy(Vector2Int matrixSize, List<List<Block>> blocksToDestroyLists)
		{
			for (int i = 0; i < steps.Count; i++)
			{
				int iteration = 1;
				while (true)
				{
					Vector2Int blockToDestroyPosition = Block.MatrixPosition + steps[i] * iteration;

					if (!IsValidPosition(blockToDestroyPosition, matrixSize))
					{
						break;
					}

					if (Block.Neighbors.TryGetValue(blockToDestroyPosition, out Block block))
					{
						blocksToDestroyLists[i].Add(block);
					}

					iteration++;
				}
			}
		}

		private List<List<Block>> InitializeLists()
		{
			List<List<Block>> blocksToDestroyLists = new(steps.Count);
			for (int i = 0; i < steps.Count; i++)
			{
				blocksToDestroyLists.Add(new List<Block>());
			}

			return blocksToDestroyLists;
		}

		private async void DestroyBlocksAsync(List<Block> blocksToDestroy)
		{
			foreach (var block in blocksToDestroy)
			{
				if (block == null)
				{
					continue;
				}

				if (block.Config.TryGetComponent(out HealthComponent healthComponent))
				{
					healthComponent.Kill();
				}
				await UniTask.Delay(System.TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}

		private bool IsValidPosition(Vector2Int position, Vector2Int matrixSize)
		{
			return position.x >= 0 && position.x <= matrixSize.x &&
				   position.y >= 0 && position.y <= matrixSize.y;
		}

		private Vector2Int GetMaxVector2Int(Dictionary<Vector2Int, Block> blocks)
		{
			if (blocks.Count == 0)
			{
				return Vector2Int.zero;
			}

			int maxX = int.MinValue;
			int maxY = int.MinValue;

			foreach (var blockPair in blocks)
			{
				Vector2Int position = blockPair.Key;

				maxX = Mathf.Max(maxX, position.x);
				maxY = Mathf.Max(maxY, position.y);
			}

			return new Vector2Int(maxX, maxY);
		}

		private Block[,] BuildMatrixFromDictionary(Dictionary<Vector2Int, Block> blocks, Vector2Int size)
		{
			Block[,] matrix = new Block[size.x, size.y];

			foreach (var blockPair in blocks)
			{
				Vector2Int position = blockPair.Key;
				matrix[position.x, position.y] = blockPair.Value;
			}

			return matrix;
		}
	}
}
