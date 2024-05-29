using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.ColorBomb
{
	public class ColorBombComponent : General.Component
	{
		[SerializeField] private float pauseBetweenExplosions;

		public override void Execute()
		{
			List<Block> neighbors = GetNeighbors();
			if (neighbors.Count <= 0)
			{
				return;
			}

			string PopularBlockName = GetMostPopularBlockName(neighbors);
			List<Block> validNeighbors = neighbors.Where(x => x.Config.BlockName.Equals(PopularBlockName)).ToList();

			List<List<Vector2Int>> blocksPositionsToDestroy = GetBlocksPositionsToDestroy(validNeighbors);

			foreach (List<Vector2Int> positions in blocksPositionsToDestroy)
			{
				DestroyAllAsync(positions);
			}
		}

		private static List<List<Vector2Int>> GetBlocksPositionsToDestroy(List<Block> validNeighbors)
		{
			List<List<Vector2Int>> blocksPositionsToDestroy = new();
			BlockSearcher searcher = new();
			foreach (Block neighbor in validNeighbors)
			{
				blocksPositionsToDestroy.Add(searcher.GetSameBlocksPositions(neighbor));
			}

			return blocksPositionsToDestroy;
		}

		private async void DestroyAllAsync(List<Vector2Int> positions)
		{
			foreach (var position in positions)
			{
				if (!Block.Neighbors.TryGetValue(position, out Block block))
				{
					continue;
				}

				if (block.Config.TryGetComponent(out HealthComponent component))
				{
					component.Kill();
				}
				await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
		}

		private string GetMostPopularBlockName(List<Block> neighbors)
		{
			Dictionary<string, int> counts = new();

			foreach (var neighbor in neighbors)
			{
				if (counts.ContainsKey(neighbor.Config.BlockName))
				{
					counts[neighbor.Config.BlockName]++;
				}
				else
				{
					counts[neighbor.Config.BlockName] = 1;
				}
			}

			return counts.OrderByDescending(pair => pair.Value).First().Key;
		}

		private List<Block> GetNeighbors()
		{
			List<Vector2Int> pattern = new()
					{
						new (0, 1),
						new (0, -1),
						new (1, 0),
						new (-1, 0),
					};

			List<Block> neighbors = new();
			foreach (var offset in pattern)
			{
				var neighborPosition = Block.MatrixPosition + offset;
				if (!Block.Neighbors.TryGetValue(neighborPosition, out Block block))
				{
					continue;
				}

				if (!block.Config.TryGetComponent(out ColorBombTargetComponent colorBombTargetComponent))
				{
					continue;
				}

				neighbors.Add(block);
			}
			return neighbors;
		}
	}
}
