using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public class BlockFactory : IBlockFactory
	{
		private BlocksDictionary blocksDictionary;
		private Block blockTemplate;
		private Transform container;

		public BlockFactory(BlocksDictionary blocksDictionary, Block blockTemplate, Transform container)
		{
			this.blocksDictionary = blocksDictionary;
			this.blockTemplate = blockTemplate;
			this.container = container;
		}

		public Block GetBlock(int id)
		{
			Block block = GameObject.Instantiate(blockTemplate, container);

			BlockConfig configTemplate = blocksDictionary.Blocks[id];
			BlockConfig newConfig = GameObject.Instantiate(configTemplate);
			newConfig.Init(block);

			block.Init(newConfig);
			return block;
		}
	}
}