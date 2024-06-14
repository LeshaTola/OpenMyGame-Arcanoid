using Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.GetBlocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.Processing;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb
{
	public class BombComponent : General.Component
	{
		[SerializeField] private IGetBlocksStrategy getBlocksStrategy;
		[SerializeField] private IBlockProcessingStrategy processBlocksStrategy;

		public override void Execute()
		{
			base.Execute();

			getBlocksStrategy.Init(Block);
			List<List<Block>> blocksToDestroyLists = getBlocksStrategy.GetBlocksLists();
			if (blocksToDestroyLists == null || blocksToDestroyLists.Count <= 0)
			{
				return;
			}

			foreach (var list in blocksToDestroyLists)
			{
				processBlocksStrategy.Process(list);
			}
		}
	}
}
