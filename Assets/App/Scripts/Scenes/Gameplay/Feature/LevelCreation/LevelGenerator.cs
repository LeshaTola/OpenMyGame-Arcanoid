using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.Progress;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private ProgressController progressController;
		[SerializeField] private LevelConfig levelConfig;

		private IFieldSizeProvider fieldController;
		private IBlockFactory blockFactory;
		private List<Block> blocks = new();

		[Inject]
		public void Construct(IFieldSizeProvider fieldController, IBlockFactory blockFactory)
		{
			this.fieldController = fieldController;
			this.blockFactory = blockFactory;
		}

		public void GenerateLevel(LevelInfo levelInfo)
		{
			if (blocks.Count > 0)
			{
				DestroyLevel();
			}

			GameField gameField = fieldController.GetGameField();
			float blockWidth = GetBlockWidth(levelInfo, gameField);

			for (int i = 0; i < levelInfo.Height; i++)
			{
				for (int j = 0; j < levelInfo.Width; j++)
				{
					Block block = PrepareBlock(levelInfo, blockWidth, i, j);
					PlaceBlock(gameField, i, j, block);
				}
			}

			progressController.Init(blocks);
		}

		public void DestroyLevel()
		{
			progressController.CleanUp();
			foreach (Block block in blocks)
			{
				Destroy(block.gameObject);
			}
			blocks.Clear();
		}

		private void PlaceBlock(GameField gameField, int i, int j, Block block)
		{
			Vector2 newPosition = GetNewBlockPosition(gameField, i, j, block);
			block.transform.position = newPosition;
		}

		private Block PrepareBlock(LevelInfo levelInfo, float blockWidth, int i, int j)
		{
			Block block = blockFactory.GetBlock(levelInfo.BlocksMatrix[j, i]);
			block.ResizeBlock(blockWidth);
			SubscribeOnBlock(block);
			blocks.Add(block);
			return block;
		}

		private void SubscribeOnBlock(Block block)
		{
			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				healthComponent.OnDeath += OnBlockDeath;
			}
		}

		private void UnsubscribeFromBlock(Block block)
		{
			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				healthComponent.OnDeath -= OnBlockDeath;
			}
		}

		private void OnBlockDeath(Block block)
		{
			blocks.Remove(block);
			UnsubscribeFromBlock(block);
		}

		private Vector2 GetNewBlockPosition(GameField gameField, int i, int j, Block block)
		{
			float horizontalOffset = gameField.MinX + block.Width / 2 + (block.Width + levelConfig.Spacing) * j;
			float verticalOffset = gameField.MaxY - block.Height / 2 - (block.Height + levelConfig.Spacing) * i;
			return new Vector2(horizontalOffset, verticalOffset);
		}

		private float GetBlockWidth(LevelInfo levelInfo, GameField gameField)
		{
			return (gameField.Width - (levelInfo.Width - 1) * levelConfig.Spacing) / levelInfo.Width;
		}
	}
}