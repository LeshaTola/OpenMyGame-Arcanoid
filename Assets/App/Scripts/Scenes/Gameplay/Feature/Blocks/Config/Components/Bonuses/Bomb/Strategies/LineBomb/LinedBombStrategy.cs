using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies
{
	public class LinedBombStrategy : IBombStrategy
	{
		[SerializeField] private List<Line> steps;

		private Block block;

		public void Init(Block block)
		{
			this.block = block;
		}

		public List<List<Block>> GetBlocksToDestroy()
		{
			return GetBlocksToDestroy(steps);
		}

		public List<List<Block>> GetBlocksToDestroy(List<Line> steps)
		{
			if (steps == null || steps.Count <= 0)
			{
				return null;
			}

			Vector2Int matrixSize = GetMaxVector2Int(block.Neighbors);

			return GetValidBlocksToDestroy(matrixSize, steps);
		}

		private List<List<Block>> GetValidBlocksToDestroy(Vector2Int matrixSize, List<Line> steps)
		{
			List<List<Block>> blocksToDestroyLists = InitializeLists(steps);

			for (int i = 0; i < steps.Count; i++)
			{
				int iteration = 1;
				while (true)
				{
					Vector2Int blockToDestroyPosition = block.MatrixPosition + steps[i].Direction * iteration;

					if (!IsValidPosition(blockToDestroyPosition, matrixSize) || IsCompete(steps[i], iteration))
					{
						break;
					}

					if (block.Neighbors.TryGetValue(blockToDestroyPosition, out Block neighborBlock))
					{
						blocksToDestroyLists[i].Add(neighborBlock);
					}

					iteration++;
				}
			}
			return blocksToDestroyLists;
		}

		private List<List<Block>> InitializeLists(List<Line> steps)
		{
			List<List<Block>> blocksToDestroyLists = new(steps.Count);
			for (int i = 0; i < steps.Count; i++)
			{
				blocksToDestroyLists.Add(new List<Block>());
			}

			return blocksToDestroyLists;
		}

		private bool IsCompete(Line step, int iteration)
		{
			return step.iterations != -1 && iteration > step.iterations;
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
	}
}
