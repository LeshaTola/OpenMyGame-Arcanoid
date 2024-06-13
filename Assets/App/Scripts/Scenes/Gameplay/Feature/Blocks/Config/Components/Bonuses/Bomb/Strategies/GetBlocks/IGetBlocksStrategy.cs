using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Bonuses.Bomb.Strategies.GetBlocks
{
	public interface IGetBlocksStrategy
	{
		public void Init(Block block);
		List<List<Block>> GetBlocksLists();
	}
}
