using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using Scenes.Gameplay.Feature.Bonuses.Configs;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.LevelCreation.Configs;
using Scenes.Gameplay.Feature.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public class LevelGenerator : ILevelGenerator
	{
		public event Action<Block> OnBlockDestroyed;

		private LevelConfig levelConfig;
		private BonusesDatabase bonusesDatabase;

		private IProgressController progressController;
		private IFieldSizeProvider fieldSizeProvider;
		private IBlockFactory blockFactory;

		private Dictionary<Vector2Int, Block> blocks = new();
		private float animationTime;
		private float blockWidth;

		public Dictionary<Vector2Int, Block> Blocks { get => blocks; }

		public LevelGenerator(IProgressController progressController,
						IFieldSizeProvider fieldSizeProvider,
						IBlockFactory blockFactory,
						LevelConfig levelConfig,
						BonusesDatabase bonusesDatabase)
		{
			this.progressController = progressController;
			this.fieldSizeProvider = fieldSizeProvider;
			this.blockFactory = blockFactory;
			this.levelConfig = levelConfig;
			this.bonusesDatabase = bonusesDatabase;
		}

		public async UniTask GenerateLevelAsync(LevelInfo levelInfo)
		{
			progressController.ProcessProgress();

			if (blocks.Count > 0)
			{
				await DestroyLevelAsync();
			}

			blockWidth = GetBlockWidth(levelInfo);
			SetAnimationTime(levelInfo);

			for (int i = 0; i < levelInfo.Height; i++)
			{
				for (int j = 0; j < levelInfo.Width; j++)
				{
					if (levelInfo.BlocksMatrix[j, i] == -1)
					{
						continue;
					}

					Block block = blockFactory.GetBlock(levelInfo.BlocksMatrix[j, i]);
					Vector2Int blockMatrixPosition = new Vector2Int(j, i);
					block.Setup(blocks, blockMatrixPosition);
					AddBonusComponent(levelInfo, i, j, block);

					Block preparedBlock = PrepareBlock(block);
					await PlaceBlockAsync(i, j, preparedBlock);
					blocks.Add(blockMatrixPosition, block);
				}
			}

			progressController.Init(new List<Block>(blocks.Values));
		}

		private void SetAnimationTime(LevelInfo levelInfo)
		{
			int blocksCount = levelInfo.BlocksMatrix.Cast<int>().Count(x => x != -1);
			animationTime = levelConfig.BuildingTime / blocksCount;
		}

		private void AddBonusComponent(LevelInfo levelInfo, int i, int j, Block block)
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

		public async UniTask DestroyLevelAsync()
		{
			progressController.CleanUp();

			foreach (Block block in blocks.Values)
			{
				await DestroyBlockAsync(block);
			}
			blocks.Clear();
		}

		private async Task DestroyBlockAsync(Block block)
		{
			Vector2 newPosition = GetTopPosition(block, block.transform.position);
			Tween animation = block.transform.DOMove(newPosition, animationTime);
			animation.SetEase(Ease.InBounce);
			await animation.AsyncWaitForCompletion();
			GameObject.Destroy(block.gameObject);
		}

		private async UniTask PlaceBlockAsync(int i, int j, Block block)
		{
			Vector2 newPosition = GetNewBlockPosition(fieldSizeProvider.GameField, i, j, block);
			block.transform.position = GetTopPosition(block, newPosition);

			var animation = block.transform.DOMove(newPosition, animationTime);
			animation.SetEase(Ease.OutBounce);
			await animation.AsyncWaitForCompletion();
		}

		private Block PrepareBlock(Block block)
		{
			block.ResizeBlock(blockWidth);
			SubscribeOnBlock(block);
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
			blocks.Remove(block.MatrixPosition);
			UnsubscribeFromBlock(block);
		}

		private Vector2 GetNewBlockPosition(GameField gameField, int i, int j, Block block)
		{
			float horizontalOffset = gameField.MinX + block.Width / 2 + (block.Width + levelConfig.Spacing) * j;
			float verticalOffset = gameField.MaxY - block.Height / 2 - (block.Height + levelConfig.Spacing) * i;
			return new Vector2(horizontalOffset, verticalOffset);
		}

		private float GetBlockWidth(LevelInfo levelInfo)
		{
			return (fieldSizeProvider.GameField.Width - (levelInfo.Width - 1) * levelConfig.Spacing) / levelInfo.Width;
		}

		private Vector2 GetTopPosition(Block block, Vector2 position)
		{
			return new Vector2(position.x, fieldSizeProvider.GameField.MaxY + block.Height);
		}
	}
}