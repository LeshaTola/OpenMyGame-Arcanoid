using App.Scripts.Scenes.Gameplay.Feature.Blocks;
using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config;
using App.Scripts.Scenes.Gameplay.Feature.Field;
using App.Scripts.Scenes.Gameplay.Feature.LevelCreation.Configs;
using App.Scripts.Scenes.Gameplay.Feature.Progress;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private FieldController fieldController;
		[SerializeField] private ProgressController progressController;
		[SerializeField] private LevelConfig levelConfig;
		[SerializeField] private BlocksDictionary blocksDictionary;
		[SerializeField] private Block blockTemplate;

		private List<Block> blocks = new();

		public void GenerateLevel(LevelInfo levelInfo)
		{
			GameField gameField = fieldController.GetGameField();
			float blockWidth = GetBlockWidth(levelInfo, gameField);

			for (int i = 0; i < levelInfo.Height; i++)
			{
				for (int j = 0; j < levelInfo.Width; j++)
				{
					Block block = GetPreparedBlock(levelInfo, blockWidth, i, j);
					blocks.Add(block);

					Vector2 newPosition = GetNewBlockPosition(gameField, i, j, block);
					block.transform.position = newPosition;
				}
			}

			progressController.Init(blocks);
		}

		private Block GetPreparedBlock(LevelInfo levelInfo, float blockWidth, int i, int j)
		{
			Block block = Instantiate(blockTemplate, transform);

			BlockConfig configTemplate = blocksDictionary.Blocks[levelInfo.BlocksMatrix[j, i]];
			BlockConfig newConfig = Instantiate(configTemplate);
			newConfig.Init(block);

			block.Init(newConfig);
			block.ResizeBlock(blockWidth);
			return block;
		}

		private Vector2 GetNewBlockPosition(GameField gameField, int i, int j, Block block)
		{
			float horizontalOffset = gameField.MinX + block.Width / 2 + (block.Width + levelConfig.Spacing) * j;
			float verticalOffset = gameField.MaxY - block.Height / 2 - (block.Height + levelConfig.Spacing) * i;
			return new Vector2(horizontalOffset, verticalOffset);
		}

		private float GetBlockWidth(LevelInfo levelInfo, GameField gameField)
		{
			return gameField.Width / levelInfo.Width - levelConfig.Spacing;
		}
	}
}