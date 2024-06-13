using Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.GetBlocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.ColorBomb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies
{
	public class ColorBombStrategy : IGetBlocksStrategy
	{
		private Block block;
		private List<Vector2Int> pattern;

		public void Init(Block block)
		{
			this.block = block;
			pattern = new()
			{
				new (0, 1),
				new (0, -1),
				new (1, 0),
				new (-1, 0),
			};
		}

		public List<List<Block>> GetBlocksLists()
		{
			List<Block> neighbors = GetNeighbors();
			if (neighbors.Count <= 0)
			{
				return null;
			}

			List<List<Block>> maxChains = GetMaxChain(neighbors);
			return maxChains;
		}

		private List<Block> GetNeighbors()
		{
			List<Block> neighbors = new();
			foreach (var offset in pattern)
			{
				var neighborPosition = block.MatrixPosition + offset;
				if (!block.Neighbors.TryGetValue(neighborPosition, out Block neighborBlock))
				{
					continue;
				}

				if (!neighborBlock.Config.TryGetComponent(out ColorBombTargetComponent colorBombTargetComponent))
				{
					continue;
				}

				neighbors.Add(neighborBlock);
			}
			return neighbors;
		}

		private List<List<Block>> GetMaxChain(List<Block> neighbors)
		{
			Dictionary<string, List<List<Block>>> chainsDictionary = new();

			BlockSearcher searcher = new();
			foreach (Block neighbor in neighbors)
			{
				if (!chainsDictionary.ContainsKey(neighbor.Config.BlockName))
				{
					chainsDictionary[neighbor.Config.BlockName] = new List<List<Block>>();
				}

				if (chainsDictionary[neighbor.Config.BlockName].FindIndex(x => x.Equals(neighbor.MatrixPosition)) == -1)
				{
					chainsDictionary[neighbor.Config.BlockName].Add(searcher.GetSameBlocks(neighbor));
				}
			}

			return GetMaxChain(chainsDictionary);
		}

		private List<List<Block>> GetMaxChain(Dictionary<string, List<List<Block>>> chainsDictionary)
		{
			int maxCount = 0;
			int tempCount = 0;
			string key = null;

			foreach (var chains in chainsDictionary)
			{
				tempCount = chains.Value.Sum(x => x.Count);
				if (tempCount > maxCount)
				{
					maxCount = tempCount;
					key = chains.Key;
				}
			}
			var maxChains = chainsDictionary[key];
			return maxChains;
		}
	}
}
