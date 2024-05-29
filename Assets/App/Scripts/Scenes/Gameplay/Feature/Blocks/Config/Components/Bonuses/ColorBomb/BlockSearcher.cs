using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.ColorBomb
{
	public class BlockSearcher
	{
		private readonly List<Vector2Int> openNodes = new();
		private readonly HashSet<Vector2Int> closedNodes = new();

		private List<Vector2Int> pattern = new()
		{
			new (0, 1),
			new (0, -1),
			new (1, 0),
			new (-1, 0),
		};

		public List<Vector2Int> GetSameBlocksPositions(Block block)
		{
			openNodes.Clear();
			closedNodes.Clear();

			openNodes.Add(block.MatrixPosition);

			while (openNodes.Count > 0)
			{
				Vector2Int currentNode = openNodes[0];

				List<Vector2Int> neighbors = GetValidNeighbors(currentNode, block.Neighbors, block.Config.BlockName);
				openNodes.AddRange(neighbors);

				openNodes.Remove(currentNode);
				closedNodes.Add(currentNode);
			}

			return new List<Vector2Int>(closedNodes);
		}

		private List<Vector2Int> GetValidNeighbors(Vector2Int currentNode, Dictionary<Vector2Int, Block> blocks, string blockName)
		{
			List<Vector2Int> neighbors = new();
			foreach (var offset in pattern)
			{
				var neighborPosition = currentNode + offset;
				if (!closedNodes.Contains(neighborPosition) && blocks.ContainsKey(neighborPosition))
				{
					if (!blocks[neighborPosition].Config.BlockName.Equals(blockName))
					{
						continue;
					}
					neighbors.Add(neighborPosition);
				}
			}
			return neighbors;
		}

	}
}
