using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using System;
using System.Collections.Generic;
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

			List<Vector2Int> maxChain = GetMaxChain(neighbors);
			DestroyAllAsync(maxChain);
		}

		private static List<Vector2Int> GetMaxChain(List<Block> neighbors)
		{
			Dictionary<string, List<Vector2Int>> chainsDictionary = new();

			BlockSearcher searcher = new();
			foreach (Block neighbor in neighbors)
			{
				if (!chainsDictionary.ContainsKey(neighbor.Config.BlockName))
				{
					chainsDictionary[neighbor.Config.BlockName] = new List<Vector2Int>();
				}

				if (chainsDictionary[neighbor.Config.BlockName].FindIndex(x => x.Equals(neighbor.MatrixPosition)) == -1)
				{
					chainsDictionary[neighbor.Config.BlockName].AddRange(searcher.GetSameBlocksPositions(neighbor));
				}
			}

			return GetMaxChain(chainsDictionary);
		}

		private static List<Vector2Int> GetMaxChain(Dictionary<string, List<Vector2Int>> chainsDictionary)
		{
			int maxListCount = 0;
			string key = null;

			foreach (var chain in chainsDictionary)
			{
				if (chain.Value.Count > maxListCount)
				{
					maxListCount = chain.Value.Count;
					key = chain.Key;
				}
			}
			var maxChain = chainsDictionary[key];
			return maxChain;
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
					SpawnExplosion(block);
					component.Kill();
				}
				await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenExplosions));
			}
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

		private void SpawnExplosion(Block blockToDamage)
		{
			var newExplosion = blockToDamage.KeyPool.Get("explosion");
			newExplosion.transform.position = blockToDamage.transform.position;
			newExplosion.Particle.Play();
		}
	}
}
