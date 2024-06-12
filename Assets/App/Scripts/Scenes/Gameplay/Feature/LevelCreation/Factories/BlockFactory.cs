using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public class BlockFactory : IBlockFactory
	{
		private BlocksDictionary blocksDictionary;
		private Block blockTemplate;
		private Transform container;
		private DiContainer diContainer;
		public BlockFactory(BlocksDictionary blocksDictionary,
					  Block blockTemplate,
					  Transform container,
					  DiContainer diContainer)
		{
			this.blocksDictionary = blocksDictionary;
			this.blockTemplate = blockTemplate;
			this.container = container;
			this.diContainer = diContainer;
		}

		public Block GetBlock(int id)
		{
			Block block = diContainer.InstantiatePrefabForComponent<Block>(blockTemplate, container);

			BlockConfig configTemplate = blocksDictionary.Blocks[id];
			BlockConfig newConfig = GameObject.Instantiate(configTemplate);
			newConfig.Init(block);

			block.Init(newConfig);
			return block;
		}
	}
}