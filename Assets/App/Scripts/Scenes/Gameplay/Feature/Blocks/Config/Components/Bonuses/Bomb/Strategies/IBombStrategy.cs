using System.Collections.Generic;
using System.Linq;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies
{
	public interface IBombStrategy
	{
		public void Init(Block block);
		List<List<Block>> GetBlocksToDestroy();
	}

	public class ClassicBomb : IBombStrategy
	{
		private LinedBombStrategy bombStrategy;
		private List<Line> steps;

		public void Init(Block block)
		{
			bombStrategy = new();
			bombStrategy.Init(block);
			InitSteps();
		}

		private void InitSteps()
		{
			steps = new List<Line>()
			{
				new()
				{
					Direction = new(1,0),
					iterations = 1,
				},
				new()
				{
					Direction = new(0,1),
					iterations = 1,
				},
				new()
				{
					Direction = new(1,1),
					iterations = 1,
				},
				new()
				{
					Direction = new(-1,0),
					iterations = 1,
				},
				new()
				{
					Direction = new(0,-1),
					iterations = 1,
				},
				new()
				{
					Direction = new(-1,-1),
					iterations = 1,
				},
				new()
				{
					Direction = new(1,-1),
					iterations = 1,
				},
				new()
				{
					Direction = new(-1,1),
					iterations = 1,
				},
			};
		}

		public List<List<Block>> GetBlocksToDestroy()
		{
			List<List<Block>> flattenBlockLists = new(1);

			List<List<Block>> blocksListsToDestroy = bombStrategy.GetBlocksToDestroy(steps);
			flattenBlockLists.Add(blocksListsToDestroy.SelectMany(blockList => blockList).ToList());
			return flattenBlockLists;
		}
	}
}
