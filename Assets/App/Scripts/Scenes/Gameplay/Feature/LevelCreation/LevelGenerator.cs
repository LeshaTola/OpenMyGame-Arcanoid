using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.Progress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public class LevelGenerator : ILevelGenerator
	{
		public event Action<Block> OnBlockDestroyed;

		private LevelConfig levelConfig;
		private BonusesDatabase bonusesDatabase;

		private IProgressController progressController;
		private IFieldSizeProvider fieldController;
		private IBlockFactory blockFactory;

		private List<Block> blocks = new();

		public LevelGenerator(IProgressController progressController,
						IFieldSizeProvider fieldController,
						IBlockFactory blockFactory,
						LevelConfig levelConfig,
						BonusesDatabase bonusesDatabase)
		{
			this.progressController = progressController;
			this.fieldController = fieldController;
			this.blockFactory = blockFactory;
			this.levelConfig = levelConfig;
			this.bonusesDatabase = bonusesDatabase;
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
					if (levelInfo.BlocksMatrix[j, i] == -1)
					{
						continue;
					}

					Block block = blockFactory.GetBlock(levelInfo.BlocksMatrix[j, i]);
					AddBonusComponent(levelInfo, i, j, block);

					Block preparedBlock = PrepareBlock(block, blockWidth);
					PlaceBlock(gameField, i, j, preparedBlock);
				}
			}

			progressController.Init(blocks);
		}

		void AddBonusComponent(LevelInfo levelInfo, int i, int j, Block block)
		{
			string bonusId = levelInfo.BonusesMatrix[j, i];

			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent == null || bonusId == null || !bonusesDatabase.Bonuses.ContainsKey(bonusId))
			{
				return;
			}

			DropBonusComponent dropBonusComponent = new(bonusId);
			block.Config.AddComponentIfNull(dropBonusComponent, healthComponent.DeathComponents);
			block.Visual.SetBonus(bonusesDatabase.Bonuses[bonusId].BlockSprite);
		}

		public void DestroyLevel()
		{
			progressController.CleanUp();
			foreach (Block block in blocks)
			{
				GameObject.Destroy(block.gameObject);
			}
			blocks.Clear();
		}

		private void PlaceBlock(GameField gameField, int i, int j, Block block)
		{
			Vector2 newPosition = GetNewBlockPosition(gameField, i, j, block);
			block.transform.position = newPosition;
		}

		private Block PrepareBlock(Block block, float blockWidth)
		{
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
			OnBlockDestroyed?.Invoke(block);
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